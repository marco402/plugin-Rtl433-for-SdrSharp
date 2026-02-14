#!/bin/bash

OWNER=$1
REPO=$2
WORKFLOW_FILE=$3
BRANCH=$4
RUN_ID=$5

echo "Waiting for workflow run $RUN_ID in $OWNER/$REPO..."

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
