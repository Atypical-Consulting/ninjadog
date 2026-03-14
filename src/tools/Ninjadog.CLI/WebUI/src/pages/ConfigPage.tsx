import { useRef, useState, useCallback } from 'react';
import { useConfigStore } from '../store/config-store';
import { useUiStore } from '../store/ui-store';
import { useNavigate } from 'react-router';
import * as api from '../api/ninjadog-api';

/* eslint-disable @typescript-eslint/no-explicit-any */

function Tooltip({ text }: { text: string }) {
  return (
    <span className="tooltip-wrapper">
      <span className="tooltip-icon">?</span>
      <span className="tooltip-content">{text}</span>
    </span>
  );
}

function FieldHint({ text }: { text: string }) {
  return (
    <p className="text-[11px] mt-1" style={{ color: 'var(--text-muted)', lineHeight: 1.4 }}>{text}</p>
  );
}

export default function ConfigPage() {
  const state = useConfigStore((s) => s.state);
  const setState = useConfigStore((s) => s.setState);
  const pushUndo = useConfigStore((s) => s.pushUndo);
  const onStateChanged = useConfigStore((s) => s.onStateChanged);
  const setTemplatePickerOpen = useUiStore((s) => s.setTemplatePickerOpen);
  const navigate = useNavigate();

  const [dirBrowserOpen, setDirBrowserOpen] = useState(false);
  const [dirData, setDirData] = useState<{ current: string; absolute: string; parent: string | null; directories: string[] } | null>(null);
  const [fieldErrors, setFieldErrors] = useState<Record<string, string>>({});
  const interacted = useRef(new Set<string>());
  const undoPushedRef = useRef(false);

  const c = (state.config || {}) as Record<string, any>;
  const cors = (c.cors || {}) as Record<string, any>;
  const features = (c.features || {}) as Record<string, any>;
  const database = (c.database || {}) as Record<string, any>;

  const updateConfig = useCallback(
    (updater: (config: Record<string, any>) => void) => {
      const newState = JSON.parse(JSON.stringify(state));
      newState.config = newState.config || {};
      updater(newState.config);
      setState(newState);
      onStateChanged();
    },
    [state, setState, onStateChanged],
  );

  const handleFocus = () => {
    undoPushedRef.current = false;
  };

  const ensureUndo = () => {
    if (!undoPushedRef.current) {
      pushUndo();
      undoPushedRef.current = true;
    }
  };

  const validateField = (field: string, value: string) => {
    const errors: Record<string, string> = { ...fieldErrors };
    if (field === 'name') {
      if (!value) errors.name = 'Project name is required';
      else if (!/^[a-zA-Z0-9.\-]+$/.test(value)) errors.name = 'Use only letters, numbers, dots, and hyphens';
      else delete errors.name;
    }
    if (field === 'rootNamespace') {
      if (value && !/^[A-Z][a-zA-Z0-9]*(\.[A-Z][a-zA-Z0-9]*)*$/.test(value))
        errors.rootNamespace = 'Invalid namespace format. Use PascalCase with dots';
      else delete errors.rootNamespace;
    }
    if (field === 'corsOrigins') {
      if (value) {
        const origins = value.split(',').map((s) => s.trim()).filter(Boolean);
        const allValid = origins.every((o) => o === '*' || /^https?:\/\//.test(o));
        if (!allValid) errors.corsOrigins = 'Origins must be URLs (http/https) or *';
        else delete errors.corsOrigins;
      } else {
        delete errors.corsOrigins;
      }
    }
    setFieldErrors(errors);
  };

  const handleChange = (field: string, value: string | boolean) => {
    interacted.current.add(field);
    ensureUndo();
    if (typeof value === 'string' && interacted.current.has(field)) {
      validateField(field, value);
    }

    updateConfig((cfg) => {
      switch (field) {
        case 'name': cfg.name = value; break;
        case 'version': cfg.version = value; break;
        case 'description': cfg.description = value; break;
        case 'rootNamespace': cfg.rootNamespace = value; break;
        case 'outputPath': cfg.outputPath = value || '.'; break;
        case 'databaseProvider':
          if (value && value !== 'sqlite') cfg.database = { provider: value };
          else delete cfg.database;
          break;
        case 'corsOrigins': {
          const origins = (value as string).split(',').map((s) => s.trim()).filter(Boolean);
          if (origins.length > 0) {
            cfg.cors = cfg.cors || {};
            cfg.cors.origins = origins;
          } else {
            delete cfg.cors;
          }
          break;
        }
        case 'corsMethods': {
          const methods = (value as string).split(',').map((s) => s.trim()).filter(Boolean);
          if (methods.length > 0) { cfg.cors = cfg.cors || {}; cfg.cors.methods = methods; }
          else if (cfg.cors) delete cfg.cors.methods;
          break;
        }
        case 'corsHeaders': {
          const headers = (value as string).split(',').map((s) => s.trim()).filter(Boolean);
          if (headers.length > 0) { cfg.cors = cfg.cors || {}; cfg.cors.headers = headers; }
          else if (cfg.cors) delete cfg.cors.headers;
          break;
        }
        case 'softDelete': {
          const auditing = cfg.features?.auditing;
          if (value || auditing) {
            cfg.features = cfg.features || {};
            if (value) cfg.features.softDelete = true; else delete cfg.features.softDelete;
          } else {
            delete cfg.features;
          }
          break;
        }
        case 'auditing': {
          const sd = cfg.features?.softDelete;
          if (value || sd) {
            cfg.features = cfg.features || {};
            if (value) cfg.features.auditing = true; else delete cfg.features.auditing;
          } else {
            delete cfg.features;
          }
          break;
        }
      }
    });
  };

  const loadDirectories = async (path: string) => {
    try {
      const data = await api.getDirectories(path);
      setDirData(data);
    } catch {
      setDirData(null);
    }
  };

  const toggleDirBrowser = () => {
    if (!dirBrowserOpen) {
      loadDirectories(c.outputPath || '.');
    }
    setDirBrowserOpen(!dirBrowserOpen);
  };

  const selectDir = (dir: string) => {
    handleChange('outputPath', dir);
    setDirBrowserOpen(false);
  };

  const entityCount = state.entities ? Object.keys(state.entities).length : 0;

  return (
    <div id="tab-config" className="tab-content-active space-y-4">
      <div className="section-card">
        <div className="section-title">
          <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" style={{ display: 'inline', verticalAlign: 'middle', marginRight: 6, opacity: 0.6 }}>
            <circle cx="12" cy="12" r="3" /><path d="M19.4 15a1.65 1.65 0 0 0 .33 1.82l.06.06a2 2 0 0 1 0 2.83 2 2 0 0 1-2.83 0l-.06-.06a1.65 1.65 0 0 0-1.82-.33 1.65 1.65 0 0 0-1 1.51V21a2 2 0 0 1-2 2 2 2 0 0 1-2-2v-.09A1.65 1.65 0 0 0 9 19.4a1.65 1.65 0 0 0-1.82.33l-.06.06a2 2 0 0 1-2.83 0 2 2 0 0 1 0-2.83l.06-.06A1.65 1.65 0 0 0 4.68 15a1.65 1.65 0 0 0-1.51-1H3a2 2 0 0 1-2-2 2 2 0 0 1 2-2h.09A1.65 1.65 0 0 0 4.6 9a1.65 1.65 0 0 0-.33-1.82l-.06-.06a2 2 0 0 1 0-2.83 2 2 0 0 1 2.83 0l.06.06A1.65 1.65 0 0 0 9 4.68a1.65 1.65 0 0 0 1-1.51V3a2 2 0 0 1 2-2 2 2 0 0 1 2 2v.09a1.65 1.65 0 0 0 1 1.51 1.65 1.65 0 0 0 1.82-.33l.06-.06a2 2 0 0 1 2.83 0 2 2 0 0 1 0 2.83l-.06.06A1.65 1.65 0 0 0 19.4 9a1.65 1.65 0 0 0 1.51 1H21a2 2 0 0 1 2 2 2 2 0 0 1-2 2h-.09a1.65 1.65 0 0 0-1.51 1z" />
          </svg>
          General
        </div>
        <div className="grid grid-cols-2 gap-4">
          <div>
            <label className="field-label">Name</label>
            <input
              data-field="name"
              className={`field-input${fieldErrors.name ? ' field-error' : ''}`}
              placeholder="e.g. TodoApp, BlogEngine"
              value={c.name || ''}
              onFocus={handleFocus}
              onChange={(e) => handleChange('name', e.target.value)}
            />
            <FieldHint text="The name of your application or project." />
            {fieldErrors.name && <div className="field-error-msg" data-error-for="name" style={{ display: 'block' }}>{fieldErrors.name}</div>}
          </div>
          <div>
            <label className="field-label">Version</label>
            <input data-field="version" className="field-input" placeholder="e.g. 1.0.0" value={c.version || ''} onFocus={handleFocus} onChange={(e) => handleChange('version', e.target.value)} />
            <FieldHint text="Semantic version of your application (major.minor.patch)." />
          </div>
          <div className="col-span-2">
            <label className="field-label">Description</label>
            <input data-field="description" className="field-input" placeholder="A brief description of your API" value={c.description || ''} onFocus={handleFocus} onChange={(e) => handleChange('description', e.target.value)} />
            <FieldHint text="A brief description of your application or project." />
          </div>
          <div>
            <label className="field-label">
              Root Namespace <Tooltip text="The root C# namespace for generated code. Example: MyCompany.Api" />
            </label>
            <input
              data-field="rootNamespace"
              className={`field-input${fieldErrors.rootNamespace ? ' field-error' : ''}`}
              placeholder="e.g. MyCompany.TodoApp"
              value={c.rootNamespace || ''}
              onFocus={handleFocus}
              onChange={(e) => handleChange('rootNamespace', e.target.value)}
            />
            <FieldHint text="The root namespace for generated code. Uses dotted PascalCase notation." />
            {fieldErrors.rootNamespace && <div className="field-error-msg" data-error-for="rootNamespace" style={{ display: 'block' }}>{fieldErrors.rootNamespace}</div>}
          </div>
          <div>
            <label className="field-label">
              Output Path <Tooltip text="Directory where generated files will be written, relative to ninjadog.json" />
            </label>
            <div className="flex gap-2">
              <input
                data-field="outputPath"
                className="field-input flex-1"
                value={c.outputPath || '.'}
                onFocus={handleFocus}
                onChange={(e) => handleChange('outputPath', e.target.value)}
              />
              <button className="btn-sm btn-ghost flex items-center gap-1 whitespace-nowrap" onClick={toggleDirBrowser}>
                <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"><path d="M22 19a2 2 0 0 1-2 2H4a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h5l2 3h9a2 2 0 0 1 2 2z" /></svg>
                Browse
              </button>
            </div>
            <FieldHint text="The path where generated files will be saved, relative to ninjadog.json." />
            {dirBrowserOpen && dirData && (
              <div className="dir-browser mt-2">
                <div className="dir-current">
                  <span>{dirData.absolute || dirData.current}</span>
                  <button className="btn-sm btn-ghost" style={{ padding: '2px 8px', fontSize: 10 }} onClick={() => setDirBrowserOpen(false)}>Close</button>
                </div>
                {dirData.parent && (
                  <button className="dir-item dir-item-parent" onClick={() => loadDirectories(dirData.parent!)}>
                    <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2"><polyline points="15 18 9 12 15 6" /></svg>
                    ..
                  </button>
                )}
                {dirData.directories.map((d) => (
                  <button key={d} className="dir-item" onClick={() => loadDirectories(dirData.current === '.' ? d : `${dirData.current}/${d}`)}>
                    <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"><path d="M22 19a2 2 0 0 1-2 2H4a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h5l2 3h9a2 2 0 0 1 2 2z" /></svg>
                    {d}
                  </button>
                ))}
                <button className="dir-item dir-item-select" onClick={() => selectDir(dirData.current)}>
                  <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2"><polyline points="20 6 9 17 4 12" /></svg>
                  Select this folder
                </button>
              </div>
            )}
          </div>
        </div>
      </div>

      <div className="section-card">
        <div className="section-title">
          <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" style={{ display: 'inline', verticalAlign: 'middle', marginRight: 6, opacity: 0.6 }}>
            <ellipse cx="12" cy="5" rx="9" ry="3" /><path d="M21 12c0 1.66-4 3-9 3s-9-1.34-9-3" /><path d="M3 5v14c0 1.66 4 3 9 3s9-1.34 9-3V5" />
          </svg>
          Database
        </div>
        <div className="grid grid-cols-2 gap-4">
          <div>
            <label className="field-label">
              Provider <Tooltip text="The database engine for generated repository code" />
            </label>
            <select data-field="databaseProvider" className="field-select" value={database.provider || 'sqlite'} onChange={(e) => handleChange('databaseProvider', e.target.value)}>
              <option value="sqlite">SQLite</option>
              <option value="postgres">PostgreSQL</option>
              <option value="sqlserver">SQL Server</option>
            </select>
            <FieldHint text="Determines SQL dialect, type mappings, and connection factory in generated code." />
          </div>
        </div>
      </div>

      <div className="section-card">
        <div className="section-title">
          <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" style={{ display: 'inline', verticalAlign: 'middle', marginRight: 6, opacity: 0.6 }}>
            <path d="M12 22s8-4 8-10V5l-8-3-8 3v7c0 6 8 10 8 10z" />
          </svg>
          CORS
        </div>
        <div>
          <label className="field-label">
            Origins (comma-separated) <Tooltip text="Allowed origins for cross-origin requests. Use * for all origins (not recommended for production)" />
          </label>
          <input
            data-field="corsOrigins"
            className={`field-input${fieldErrors.corsOrigins ? ' field-error' : ''}`}
            placeholder="e.g. http://localhost:3000, https://myapp.com"
            value={(cors.origins || []).join(', ')}
            onFocus={handleFocus}
            onChange={(e) => handleChange('corsOrigins', e.target.value)}
          />
          <FieldHint text="Allowed CORS origins. Use URLs (http/https) or * for all origins." />
          {fieldErrors.corsOrigins && <div className="field-error-msg" style={{ display: 'block' }}>{fieldErrors.corsOrigins}</div>}
        </div>
        <div className="grid grid-cols-2 gap-4 mt-3">
          <div>
            <label className="field-label">Methods (comma-separated)</label>
            <input className="field-input" placeholder="e.g. GET, POST, PUT, DELETE" value={(cors.methods || []).join(', ')} onFocus={handleFocus} onChange={(e) => handleChange('corsMethods', e.target.value)} />
            <FieldHint text="Allowed HTTP methods. Omit to allow all methods." />
          </div>
          <div>
            <label className="field-label">Headers (comma-separated)</label>
            <input className="field-input" placeholder="e.g. Content-Type, Authorization" value={(cors.headers || []).join(', ')} onFocus={handleFocus} onChange={(e) => handleChange('corsHeaders', e.target.value)} />
            <FieldHint text="Allowed HTTP headers. Omit to allow all headers." />
          </div>
        </div>
      </div>

      <div className="section-card">
        <div className="section-title">
          <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" style={{ display: 'inline', verticalAlign: 'middle', marginRight: 6, opacity: 0.6 }}>
            <path d="M12 20V10" /><path d="M18 20V4" /><path d="M6 20v-4" />
          </svg>
          Features
        </div>
        <div className="space-y-3">
          <label className="flex items-start gap-3 text-sm cursor-pointer">
            <input type="checkbox" data-field="softDelete" className="field-checkbox mt-0.5" checked={!!features.softDelete} onChange={(e) => handleChange('softDelete', e.target.checked)} />
            <div>
              <span style={{ color: 'var(--text)' }}>Soft Delete</span>
              <p className="text-[11px] mt-0.5" style={{ color: 'var(--text-muted)' }}>Adds IsDeleted and DeletedAt columns. Filters deleted records from queries instead of permanently removing them.</p>
            </div>
          </label>
          <label className="flex items-start gap-3 text-sm cursor-pointer">
            <input type="checkbox" data-field="auditing" className="field-checkbox mt-0.5" checked={!!features.auditing} onChange={(e) => handleChange('auditing', e.target.checked)} />
            <div>
              <span style={{ color: 'var(--text)' }}>Auditing</span>
              <p className="text-[11px] mt-0.5" style={{ color: 'var(--text-muted)' }}>Adds CreatedAt and UpdatedAt timestamps, automatically managed on insert and update.</p>
            </div>
          </label>
        </div>
      </div>

      {entityCount === 0 && (
        <div className="empty-state">
          <div className="empty-state-icon">
            <svg viewBox="0 0 24 24" width="64" height="64" fill="none" stroke="currentColor" strokeWidth="1.5">
              <rect x="3" y="3" width="7" height="7" rx="1" /><rect x="14" y="3" width="7" height="7" rx="1" /><rect x="3" y="14" width="7" height="7" rx="1" /><path d="M17.5 14v7" /><path d="M14 17.5h7" />
            </svg>
          </div>
          <div className="empty-state-title">Ready to build your API</div>
          <div className="empty-state-text">Configure your project settings above, then add entities to generate a complete CRUD Web API.</div>
          <div className="empty-state-actions">
            <button id="btn-start-template" className="btn-sm btn-primary" onClick={() => setTemplatePickerOpen(true)}>Start from Template</button>
            <button id="btn-start-scratch" className="btn-sm btn-ghost" onClick={() => navigate('/entities')}>Add First Entity</button>
          </div>
        </div>
      )}
    </div>
  );
}
