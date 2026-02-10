'use client';

import { useState, useCallback, useEffect } from 'react';
import { ApplicationStatus, WorkflowAction } from '../types/entities';

interface UseWorkflowReturn {
  status: ApplicationStatus | null;
  availableActions: WorkflowAction[];
  performAction: (action: WorkflowAction, data?: any) => Promise<void>;
  loading: boolean;
  error: string | null;
}

export function useWorkflow(applicationId: string): UseWorkflowReturn {
  const [status, setStatus] = useState<ApplicationStatus | null>(null);
  const [availableActions, setAvailableActions] = useState<WorkflowAction[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  // Fetch current status and available actions
  useEffect(() => {
    const fetchWorkflowData = async () => {
      try {
        setLoading(true);
        const response = await fetch(`/api/v1/applications/${applicationId}/workflow`);
        if (response.ok) {
          const data = await response.json();
          setStatus(data.status);
          setAvailableActions(data.availableActions || []);
        }
      } catch (err) {
        setError(err instanceof Error ? err.message : 'Failed to fetch workflow');
      } finally {
        setLoading(false);
      }
    };

    fetchWorkflowData();
  }, [applicationId]);

  const performAction = useCallback(async (action: WorkflowAction, data?: any) => {
    try {
      setLoading(true);
      const response = await fetch(`/api/v1/applications/${applicationId}/workflow`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ action, data })
      });

      if (response.ok) {
        const result = await response.json();
        setStatus(result.status);
        setAvailableActions(result.availableActions || []);
      } else {
        throw new Error('Action failed');
      }
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Action failed');
      throw err;
    } finally {
      setLoading(false);
    }
  }, [applicationId]);

  return { status, availableActions, performAction, loading, error };
}
