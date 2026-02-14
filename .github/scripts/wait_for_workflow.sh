#!/bin/bash

OWNER=$1
REPO=$2
WORKFLOW_NAME=$3

echo "Waiting for workflow '$WORKFLOW_NAME' in $OWNER/$REPO..."

while true; do
  RUN=$(curl -s \
    -H "Accept: application/vnd.github+json" \
    -H "Authorization: Bearer $GITHUB_TOKEN" \
    "https://api.github.com/repos/$OWNER/$REPO/actions/runs?per_page=1")

  STATUS=$(echo "$RUN" | jq -r '.workflow_runs[0].status')
  CONCLUSION=$(echo "$RUN" | jq -r '.workflow_runs[0].conclusion')

  echo "Status: $STATUS, Conclusion: $CONCLUSION"

  if [[ "$STATUS" == "completed" ]]; then
    if [[ "$CONCLUSION" == "success" ]]; then
      echo "Workflow completed successfully."
      exit 0
    else
      echo "Workflow failed."
      exit 1
    fi
  fi

  sleep 10
done
