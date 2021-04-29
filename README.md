# Azure Batch Playground

Build and deploy app packages to Azure Batch in an automated fashion

## Workflows

* simple-cicd.yml: Doesn't work because `az batch application package create` doesn't like absolute paths. See https://github.com/Azure/cli/issues/38 for my bug report.
* simple-cicd-windows.yml: Doesn't work at all because https://github.com/marketplace/actions/azure-cli-action requires Linux.
* simple-cicd multijob.yml: Works because I upload with absolute path in build, and then download in the deploy job to a relative path. Intended for push to main to auto-deploy for dev stage.
* release-based-cd.yml: on release, build, then deploy with environments to test and then prod.

## General Links

* Documentation: https://docs.microsoft.com/en-us/azure/batch/
* C# Samples: https://github.com/Azure-Samples/azure-batch-samples/tree/master/CSharp
* Batch Explorer: https://github.com/Azure/BatchExplorer

## Application Packages

https://docs.microsoft.com/en-us/azure/batch/batch-application-packages#understand-applications-and-application-packages

