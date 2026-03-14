import { useState, useEffect } from 'react';
import { getValidationResult, onValidationChange, type Diagnostic } from '../hooks/useValidation';

function severityLabel(severity: number) {
  if (severity === 2) return 'error';
  if (severity === 1) return 'warning';
  return 'info';
}

function severityColor(severity: number) {
  if (severity === 2) return 'var(--danger)';
  if (severity === 1) return 'var(--warning)';
  return '#60a5fa';
}

function severityBg(severity: number) {
  if (severity === 2) return 'rgba(229, 77, 77, 0.08)';
  if (severity === 1) return 'rgba(229, 168, 66, 0.08)';
  return 'rgba(96, 165, 250, 0.08)';
}

function severityIcon(severity: number) {
  if (severity === 2) {
    return (
      <svg width="12" height="12" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2.5" strokeLinecap="round" strokeLinejoin="round">
        <circle cx="12" cy="12" r="10" /><line x1="15" y1="9" x2="9" y2="15" /><line x1="9" y1="9" x2="15" y2="15" />
      </svg>
    );
  }
  if (severity === 1) {
    return (
      <svg width="12" height="12" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2.5" strokeLinecap="round" strokeLinejoin="round">
        <path d="M10.29 3.86L1.82 18a2 2 0 0 0 1.71 3h16.94a2 2 0 0 0 1.71-3L13.71 3.86a2 2 0 0 0-3.42 0z" /><line x1="12" y1="9" x2="12" y2="13" /><line x1="12" y1="17" x2="12.01" y2="17" />
      </svg>
    );
  }
  return (
    <svg width="12" height="12" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2.5" strokeLinecap="round" strokeLinejoin="round">
      <circle cx="12" cy="12" r="10" /><line x1="12" y1="16" x2="12" y2="12" /><line x1="12" y1="8" x2="12.01" y2="8" />
    </svg>
  );
}

export default function ValidationPanel() {
  const [diagnostics, setDiagnostics] = useState<Diagnostic[]>([]);

  useEffect(() => {
    const update = () => {
      const result = getValidationResult();
      setDiagnostics(result?.diagnostics || []);
    };
    update();
    return onValidationChange(update);
  }, []);

  const errorCount = diagnostics.filter((d) => d.severity === 2).length;
  const warningCount = diagnostics.filter((d) => d.severity === 1).length;

  return (
    <div id="validation-panel" className="shrink-0 max-h-48 overflow-auto" style={{ borderTop: '1px solid var(--border)', background: 'var(--bg-base)' }}>
      <div
        className="flex items-center justify-between px-4 py-2 shrink-0"
        style={{ borderBottom: '1px solid var(--border-dim)', background: 'rgba(12, 13, 19, 0.6)' }}
      >
        <span className="text-[11px] font-medium uppercase tracking-widest font-mono" style={{ color: 'var(--text-muted)' }}>
          Validation
        </span>
        {diagnostics.length > 0 && (
          <div className="flex items-center gap-3">
            {errorCount > 0 && (
              <span className="flex items-center gap-1 text-[11px] font-medium" style={{ color: 'var(--danger)' }}>
                {severityIcon(2)} {errorCount} error{errorCount !== 1 ? 's' : ''}
              </span>
            )}
            {warningCount > 0 && (
              <span className="flex items-center gap-1 text-[11px] font-medium" style={{ color: 'var(--warning)' }}>
                {severityIcon(1)} {warningCount} warning{warningCount !== 1 ? 's' : ''}
              </span>
            )}
          </div>
        )}
      </div>
      <div className="px-3 py-2 space-y-1">
        {diagnostics.length === 0 ? (
          <div className="flex items-center gap-2 text-xs py-1" style={{ color: 'var(--text-muted)' }}>
            <svg width="12" height="12" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2.5" strokeLinecap="round" strokeLinejoin="round">
              <path d="M22 11.08V12a10 10 0 1 1-5.93-9.14" /><polyline points="22 4 12 14.01 9 11.01" />
            </svg>
            No validation issues
          </div>
        ) : (
          diagnostics.map((d, i) => (
            <div
              key={i}
              className="flex items-start gap-2 text-xs px-2 py-1.5 rounded"
              style={{ background: severityBg(d.severity), color: severityColor(d.severity) }}
            >
              <span className="shrink-0 mt-0.5">{severityIcon(d.severity)}</span>
              <div className="flex flex-col gap-0.5 min-w-0">
                <div className="flex items-center gap-2">
                  {d.code && (
                    <span className="font-mono font-semibold text-[10px] uppercase opacity-70">{d.code}</span>
                  )}
                  <span className="font-mono text-[10px] opacity-50">{d.path}</span>
                </div>
                <span style={{ color: 'var(--text)' }}>{d.message}</span>
              </div>
              <span
                className="shrink-0 text-[9px] uppercase font-semibold px-1.5 py-0.5 rounded-sm ml-auto"
                style={{ background: severityBg(d.severity), opacity: 0.7 }}
              >
                {severityLabel(d.severity)}
              </span>
            </div>
          ))
        )}
      </div>
    </div>
  );
}
