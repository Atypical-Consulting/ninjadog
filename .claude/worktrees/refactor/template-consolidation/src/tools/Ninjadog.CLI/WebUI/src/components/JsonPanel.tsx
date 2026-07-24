import { useEffect, useRef, useCallback } from 'react';
import Editor, { type OnMount } from '@monaco-editor/react';
import type { editor as monacoEditor } from 'monaco-editor';
import { useConfigStore } from '../store/config-store';
import { useUiStore } from '../store/ui-store';

interface JsonPanelProps {
  width: string;
}

export default function JsonPanel({ width }: JsonPanelProps) {
  const state = useConfigStore((s) => s.state);
  const getJson = useConfigStore((s) => s.getJson);
  const pushUndo = useConfigStore((s) => s.pushUndo);
  const setState = useConfigStore((s) => s.setState);
  const onStateChanged = useConfigStore((s) => s.onStateChanged);
  const showToast = useUiStore((s) => s.showToast);

  const editorRef = useRef<monacoEditor.IStandaloneCodeEditor | null>(null);
  const isUpdatingFromState = useRef(false);
  const debounceTimer = useRef<ReturnType<typeof setTimeout> | null>(null);

  const handleMount: OnMount = useCallback(
    (editor, monaco) => {
      editorRef.current = editor;

      // Define custom theme
      monaco.editor.defineTheme('ninjadog-dark', {
        base: 'vs-dark',
        inherit: true,
        rules: [
          { token: 'string.key.json', foreground: 'ff6b8a' },
          { token: 'string.value.json', foreground: '34d399' },
          { token: 'number', foreground: 'fbbf24' },
          { token: 'keyword', foreground: 'a78bfa' },
        ],
        colors: {
          'editor.background': '#0c0d13',
          'editor.foreground': '#e4e7f1',
          'editor.lineHighlightBackground': '#1c203040',
          'editorLineNumber.foreground': '#5c6075',
          'editorLineNumber.activeForeground': '#8b90a8',
          'editor.selectionBackground': '#e8553d30',
          'editorCursor.foreground': '#e8553d',
          'editorWidget.background': '#151821',
          'editorWidget.border': '#282d42',
          'input.background': '#0e1017',
          'input.border': '#1e2235',
          'editorSuggestWidget.background': '#151821',
          'editorSuggestWidget.border': '#282d42',
          'editorSuggestWidget.selectedBackground': '#242838',
          'editorGutter.background': '#0c0d13',
        },
      });
      monaco.editor.setTheme('ninjadog-dark');

      // Load schema for validation
      fetch('/api/schema')
        .then((r) => r.json())
        .then((schema) => {
          monaco.languages.json.jsonDefaults.setDiagnosticsOptions({
            validate: true,
            schemas: [
              {
                uri: 'https://ninjadog.dev/schemas/ninjadog.schema.json',
                fileMatch: ['*'],
                schema,
              },
            ],
            allowComments: false,
            trailingCommas: 'error',
          });
        })
        .catch(() => {
          // Schema fetch failed — editor still works
        });

      // Listen for user edits
      editor.onDidChangeModelContent(() => {
        if (isUpdatingFromState.current) return;
        if (debounceTimer.current) clearTimeout(debounceTimer.current);
        debounceTimer.current = setTimeout(() => {
          try {
            const parsed = JSON.parse(editor.getValue());
            pushUndo();
            setState(parsed);
            onStateChanged();
          } catch {
            // Invalid JSON
          }
        }, 300);
      });

      // Force layout after mount to ensure correct sizing
      setTimeout(() => editor.layout(), 50);
    },
    [pushUndo, setState, onStateChanged],
  );

  // Sync state → editor
  useEffect(() => {
    const editor = editorRef.current;
    if (!editor) return;

    const json = getJson();
    const text = JSON.stringify(json, null, 2);
    if (text === editor.getValue()) return;

    isUpdatingFromState.current = true;
    const scrollTop = editor.getScrollTop();
    editor.setValue(text);
    editor.setScrollTop(scrollTop);
    isUpdatingFromState.current = false;
  }, [state, getJson]);

  const handleCopy = async () => {
    const json = getJson();
    const text = JSON.stringify(json, null, 2);
    try {
      await navigator.clipboard.writeText(text);
      showToast('Copied!', 'success', 2000);
    } catch {
      showToast('Copy failed', 'error');
    }
  };

  return (
    <div
      className="flex flex-col"
      style={{
        width,
        height: '100%',
        flexGrow: width === '100%' ? 1 : 0,
        flexShrink: width === '100%' ? 1 : 0,
        borderLeft: '1px solid var(--border)',
      }}
    >
      <div
        className="flex items-center justify-between px-4 shrink-0"
        style={{ height: 42, borderBottom: '1px solid var(--border)', background: 'var(--bg-base)' }}
      >
        <span className="text-[11px] font-mono font-medium uppercase tracking-widest" style={{ color: 'var(--text-muted)' }}>
          JSON Editor
        </span>
        <button className="copy-btn" title="Copy to clipboard" onClick={handleCopy}>
          <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"><rect x="9" y="9" width="13" height="13" rx="2" ry="2" /><path d="M5 15H4a2 2 0 0 1-2-2V4a2 2 0 0 1 2-2h9a2 2 0 0 1 2 2v1" /></svg>
        </button>
      </div>
      <div id="monaco-container" style={{ flex: 1, minHeight: 0, overflow: 'hidden' }}>
        <Editor
          defaultLanguage="json"
          defaultValue={JSON.stringify(getJson(), null, 2)}
          theme="vs-dark"
          onMount={handleMount}
          options={{
            fontSize: 12.5,
            fontFamily: "'IBM Plex Mono', 'JetBrains Mono', monospace",
            minimap: { enabled: false },
            lineNumbers: 'on',
            folding: true,
            foldingStrategy: 'indentation',
            scrollBeyondLastLine: false,
            automaticLayout: true,
            padding: { top: 12, bottom: 12 },
            renderLineHighlight: 'line',
            scrollbar: { verticalScrollbarSize: 6, horizontalScrollbarSize: 6 },
            tabSize: 2,
            wordWrap: 'on',
          }}
        />
      </div>
    </div>
  );
}
