#!/bin/sh

#variables
REGION=$(printf '%s\n' "${AWS_REGION}")
SERVICE_NAME=$(printf '%s\n' "${SERVICE_NAME}")
CLUSTER_NAME=$(printf '%s\n' "${CLUSTER_NAME}")
REPOSITORY_URI_COMMIT=$(printf '%s\n' "${REPOSITORY_URI_COMMIT}")

#getting old task definition from full list
TASK_DEF_OLD_ARN=$(aws ecs list-task-definitions | jq -r ' .taskDefinitionArns[] | select( . | contains("'${SERVICE_NAME}'"))' | tail -1)

#getting old task definition information
TASK_DEF_OLD=$(aws ecs describe-task-definition --task-definition $TASK_DEF_OLD_ARN)

#generating new task definition json
TASK_DEF_NEW=$(echo $TASK_DEF_OLD | jq ' .taskDefinition | .containerDefinitions[].image = "'${REPOSITORY_URI_COMMIT}'" | del(.status, .taskDefinitionArn, .requiresAttributes, .compatibilities, .revision, .registeredAt, .registeredBy) ');

#saving to a file
echo $TASK_DEF_NEW > /tmp/$SERVICE_NAME.json

#creating new task definition
aws ecs register-task-definition --cli-input-json file:///tmp/$SERVICE_NAME.json

#getting new task definition arn
TASK_DEF_NEW_ARN=$(aws ecs list-task-definitions | jq -r ' .taskDefinitionArns[] | select( . | contains("'${SERVICE_NAME}'")) ' | tail -1)

#updating service with new task definition
aws ecs update-service \
--cluster $CLUSTER_NAME \
--service $SERVICE_NAME \
--region $REGION \
--task-definition $TASK_DEF_NEW_ARN
