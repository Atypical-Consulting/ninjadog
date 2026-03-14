import { useConfigStore } from '../store/config-store';
import { useChatStore } from '../store/chat-store';
import { useUiStore } from '../store/ui-store';

export default function Header() {
  const dirty = useConfigStore((s) => s.dirty);
  const undoStack = useConfigStore((s) => s.undoStack);
  const redoStack = useConfigStore((s) => s.redoStack);
  const undo = useConfigStore((s) => s.undo);
  const redo = useConfigStore((s) => s.redo);
  const saveConfig = useConfigStore((s) => s.saveConfig);
  const buildConfig = useConfigStore((s) => s.buildConfig);
  const getJson = useConfigStore((s) => s.getJson);
  const autoSaveEnabled = useConfigStore((s) => s.autoSaveEnabled);
  const setAutoSave = useConfigStore((s) => s.setAutoSave);
  const showToast = useUiStore((s) => s.showToast);
  const toggleShortcuts = useUiStore((s) => s.toggleShortcuts);
  const connected = useUiStore((s) => s.connected);
  const showReconnected = useUiStore((s) => s.showReconnected);
  const showBuildConsole = useUiStore((s) => s.showBuildConsole);
  const toggleChat = useChatStore((s) => s.toggle);
  const chatOpen = useChatStore((s) => s.open);

  const handleSave = async () => {
    try {
      await saveConfig();
      showToast('Configuration saved', 'success');
    } catch (err) {
      showToast('Save failed: ' + ((err as Error).message || 'Unknown error'), 'error');
    }
  };

  const handleBuild = async () => {
    try {
      const result = await buildConfig();
      showBuildConsole([result.message || (result.success ? 'Build completed.' : 'Build failed.')]);
      showToast(result.success ? 'Build succeeded' : 'Build failed', result.success ? 'success' : 'error');
    } catch (err) {
      showToast('Build failed: ' + ((err as Error).message || 'Unknown error'), 'error');
    }
  };

  const handleExport = () => {
    const json = getJson();
    const text = JSON.stringify(json, null, 2);
    const blob = new Blob([text], { type: 'application/json' });
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = 'ninjadog.json';
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
    URL.revokeObjectURL(url);
    showToast('Configuration exported', 'success');
  };

  return (
    <header className="header-bar flex items-center justify-between px-6 py-3 shrink-0">
      <div className="flex items-center gap-3">
        <div className="ninja-icon" aria-hidden="true">
          {/* Shuriken / ninja star icon */}
          <svg viewBox="0 0 24 24" width="28" height="28" fill="currentColor">
            <path d="M12 2L9 9L2 12L9 15L12 22L15 15L22 12L15 9L12 2Z" />
          </svg>
        </div>
        <div className="flex flex-col leading-tight">
          <div className="flex items-center gap-2">
            <h1 className="text-lg font-display tracking-wide text-white">NINJADOG</h1>
            {dirty && <span id="dirty-indicator" className="dirty-dot" title="Unsaved changes" />}
          </div>
          <span className="text-[10px] uppercase tracking-[0.2em] -mt-0.5" style={{ color: 'var(--text-muted)' }}>
            Config Builder
          </span>
        </div>
        {!connected && (
          <span className="status-badge text-xs px-2 py-0.5 rounded-full ml-2" style={{ background: 'rgba(229, 77, 77, 0.15)', color: '#f87171', border: '1px solid rgba(229, 77, 77, 0.3)' }}>
            Server disconnected
          </span>
        )}
        {connected && showReconnected && (
          <span className="status-badge text-xs px-2 py-0.5 rounded-full ml-2" style={{ background: 'rgba(34, 196, 129, 0.15)', color: '#4ade80', border: '1px solid rgba(34, 196, 129, 0.3)' }}>
            Reconnected
          </span>
        )}
      </div>

      <div className="flex items-center gap-2">
        <button
          id="btn-undo"
          className="header-btn-icon"
          title="Undo (Ctrl+Z)"
          disabled={undoStack.length === 0}
          onClick={undo}
        >
          {/* Undo arrow */}
          <svg width="15" height="15" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2.2" strokeLinecap="round" strokeLinejoin="round"><polyline points="1 4 1 10 7 10" /><path d="M3.51 15a9 9 0 1 0 2.13-9.36L1 10" /></svg>
        </button>
        <button
          id="btn-redo"
          className="header-btn-icon"
          title="Redo (Ctrl+Y)"
          disabled={redoStack.length === 0}
          onClick={redo}
        >
          {/* Redo arrow */}
          <svg width="15" height="15" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2.2" strokeLinecap="round" strokeLinejoin="round"><polyline points="23 4 23 10 17 10" /><path d="M20.49 15a9 9 0 1 1-2.13-9.36L23 10" /></svg>
        </button>
        <div className="header-separator" />

        <label className="auto-save-toggle" title="Auto-save">
          <input
            type="checkbox"
            checked={autoSaveEnabled}
            onChange={(e) => setAutoSave(e.target.checked)}
          />
          <span className="auto-save-label">Auto</span>
        </label>

        <button id="btn-export" className="header-btn-icon" title="Download JSON" onClick={handleExport}>
          {/* Download icon */}
          <svg width="15" height="15" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2.2" strokeLinecap="round" strokeLinejoin="round"><path d="M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4" /><polyline points="7 10 12 15 17 10" /><line x1="12" y1="15" x2="12" y2="3" /></svg>
        </button>

        <button id="btn-shortcuts" className="header-btn-icon" title="Keyboard Shortcuts (?)" onClick={toggleShortcuts}>
          {/* Keyboard icon */}
          <svg width="15" height="15" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2.2" strokeLinecap="round" strokeLinejoin="round"><rect x="2" y="4" width="20" height="16" rx="2" ry="2" /><path d="M6 8h.001" /><path d="M10 8h.001" /><path d="M14 8h.001" /><path d="M18 8h.001" /><path d="M6 12h.001" /><path d="M10 12h.001" /><path d="M14 12h.001" /><path d="M18 12h.001" /><path d="M8 16h8" /></svg>
        </button>
        <button
          id="btn-ai"
          className={`header-btn-icon${chatOpen ? ' header-btn-icon-active' : ''}`}
          title="AI Assistant (Ctrl+Shift+A)"
          onClick={toggleChat}
          style={chatOpen ? { color: 'var(--accent)', borderColor: 'var(--accent)', background: 'var(--accent-dim)' } : undefined}
        >
          {/* Chat/message bubble icon */}
          <svg width="15" height="15" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2.2" strokeLinecap="round" strokeLinejoin="round"><path d="M21 15a2 2 0 0 1-2 2H7l-4 4V5a2 2 0 0 1 2-2h14a2 2 0 0 1 2 2z" /><path d="M8 10h.01" /><path d="M12 10h.01" /><path d="M16 10h.01" /></svg>
        </button>
        <div className="header-separator" />

        <button id="btn-save" className={`header-btn header-btn-secondary${dirty ? ' header-btn-dirty' : ''}`} onClick={handleSave}>
          {/* Save/disk icon */}
          <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"><path d="M19 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h11l5 5v11a2 2 0 0 1-2 2z" /><polyline points="17 21 17 13 7 13 7 21" /><polyline points="7 3 7 8 15 8" /></svg>
          Save
        </button>
        <button id="btn-build" className="header-btn header-btn-primary" onClick={handleBuild}>
          {/* Hammer/build icon */}
          <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"><path d="M15 12l-8.5 8.5c-.83.83-2.17.83-3 0 0 0 0 0 0 0a2.12 2.12 0 0 1 0-3L12 9" /><path d="M17.64 15L22 10.64" /><path d="M20.91 11.7l-1.25-1.25c-.6-.6-.93-1.4-.93-2.25V6.5l-3-2.5-2 2 1 1.5V9c0 .85-.33 1.65-.93 2.25L12.55 12.5" /></svg>
          Build
        </button>
      </div>
    </header>
  );
}
