# GitHubActionsAlexaDemo

This project demonstrates GitHub Actions integration with Alexa and OpenAI for automated issue management with Chuck Norris-style humor!

## Features

### 🎤 Alexa Issue Creation
- Create GitHub issues using voice commands through Alexa
- Automatically generates Chuck Norris-style job titles and descriptions using OpenAI
- Integrates with Azure Functions for real-time processing

### 🥋 Chuck Norris Issue Assignment Greeting
- **NEW!** Automatically greets users with funny Chuck Norris quotes when they're assigned to an issue
- Uses OpenAI to generate personalized, developer-themed motivational quotes
- Posts encouraging comments to keep the team motivated

## Setup Instructions

1. Register an Alexa app on Amazon Alexa Developer Console https://developer.amazon.com/alexa
2. Create a classic GH PAT Token
    ![image](https://github.com/user-attachments/assets/4d0fcd2c-6dbd-438c-b4b8-31265771b069)
3. Deploy code to Azure or use ngrok to make your local azure function available on the internet
4. Add your OpenAI API key to the repository secrets as `OPENAI_API_KEY`

## Workflows

- **Alexa Webhook** (`.github/workflows/alexa_webhook.yml`): Handles voice-triggered issue creation
- **Issue Assignment Greeting** (`.github/workflows/issue-assignment-greeting.yml`): Automatically posts Chuck Norris quotes when issues are assigned
