#!/bin/bash

OWNER=$1
REPO=$2
WORKFLOW_FILE=$3
BRANCH=$4
START_TIME=$5

echo "Waiting for workflow in $OWNER/$REPO on branch $BRANCH..."

# 1. Attendre que le run apparaisse
while true; do
  RUNS=$(curl -s \
    -H "Accept: application/vnd.github+json" \
    -H "Authorization: Bearer $GITHUB_TOKEN" \
    "https://api.github.com/repos/$OWNER/$REPO/"actions/workflows/$WORKFLOW_FILE/runs?per_page=5"")

  RUN_ID=$(echo "$RUNS" | jq -r ".workflow_runs[] 
  | select(.head_branch == \"$BRANCH\") 
  | select(.created_at > \"$START_TIME\") 
  | .id" | head -n 1)


  if [[ -n "$RUN_ID" ]]; then
    echo "Detected run ID: $RUN_ID"
    break
  fi

  echo "Waiting for run to appear..."
  sleep 5
done

# 2. Attendre la fin du run
while true; do
  RUN=$(curl -s \
    -H "Accept: application/vnd.github+json" \
    -H "Authorization: Bearer $GITHUB_TOKEN" \
    "https://api.github.com/repos/$OWNER/$REPO/actions/runs/$RUN_ID")

  STATUS=$(echo "$RUN" | jq -r '.status')
  CONCLUSION=$(echo "$RUN" | jq -r '.conclusion')

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
