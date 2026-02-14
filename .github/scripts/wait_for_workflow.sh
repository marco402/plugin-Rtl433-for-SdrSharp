#!/bin/bash

OWNER=$1
REPO=$2
WORKFLOW_FILE=$3
BRANCH=$4
START_TIME=$5

echo "Waiting for workflow in $OWNER/$REPO on branch $BRANCH..."

while true; do
  RUNS=$(curl -s \
    -H "Accept: application/vnd.github+json" \
    -H "Authorization: Bearer $GITHUB_TOKEN" \
    "https://api.github.com/repos/$OWNER/$REPO/actions/workflows/$WORKFLOW_FILE/runs?branch=$BRANCH&per_page=5")

  RUN_ID=$(echo "$RUNS" | jq -r ".workflow_runs[] | select(.created_at > \"$START_TIME\") | .id" | head -n 1)

  if [[ -n "$RUN_ID" ]]; then
    echo "Detected run ID: $RUN_ID"
    break
  fi

  echo "Waiting for run to appear..."
  sleep 5
done

# Maintenant on attend la fin du run
while true; do
  RUN=$(curl -s \
    -H "Accept: application/vnd.github+json" \
    -H "Authorization: Bearer $GITHUB_TOKEN" \
    "https://api.github.com/repos/$OWNER/$REPO/actions/runs/$RUN_ID")

  STATUS=$(echo "$RUN" | jq -r '.status')
  CONCLUSION=$(echo "$RUN" | jq -r '.conclusion')

  echo "Status: $STATUS, Conclusion: $CONCLUSION"

  if [[ "$STATUS" == "completed" ]]; then
    [[ "$CONCLUSION" == "success" ]] && exit 0 || exit 1
  fi

  sleep 10
done
