#!/bin/bash

OWNER=$1
REPO=$2
ARTIFACT_NAME=$3

echo "Downloading artifact $ARTIFACT_NAME from $OWNER/$REPO..."

RUNS=$(curl -s \
  -H "Accept: application/vnd.github+json" \
  -H "Authorization: Bearer $GITHUB_TOKEN" \
  "https://api.github.com/repos/$OWNER/$REPO/actions/artifacts")

ARTIFACT_ID=$(echo "$RUNS" | jq -r ".artifacts[] | select(.name == \"$ARTIFACT_NAME\") | .id")

if [[ -z "$ARTIFACT_ID" ]]; then
  echo "Artifact $ARTIFACT_NAME not found."
  exit 1
fi

curl -L \
  -H "Authorization: Bearer $GITHUB_TOKEN" \
  -o "$ARTIFACT_NAME.zip" \
  "https://api.github.com/repos/$OWNER/$REPO/actions/artifacts/$ARTIFACT_ID/zip"

unzip -o "$ARTIFACT_NAME.zip" -d "$ARTIFACT_NAME"
