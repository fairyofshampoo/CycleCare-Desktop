# CycleCare Desktop Application

This repository hosts all the source code and resources related to the development and implementation of CycleCare, a desktop application built using WPF (Windows Presentation Foundation). CycleCare connects to a RESTful API to provide its functionalities.

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)
- [Configuration](#configuration)
- [API Integration](#api-integration)
- [Contributing](#contributing)
- [License](#license)

## Overview

CycleCare is a desktop application designed to help users track and manage their menstrual cycle. The application provides insights and analytics to help users understand their cycle patterns. CycleCare connects to a RESTful API to retrieve and store data.

## Features

- User authentication (sign in and sign up)
- Track menstrual cycles
- Log symptoms and health data
- Predict future cycles
- Provide health insights and statistics
- Connects to a REST API for data management
- User-friendly interface built with WPF

## Prerequisites

Before you begin, ensure you have the following installed:

- [.NET Framework](https://dotnet.microsoft.com/download)
- [Visual Studio](https://visualstudio.microsoft.com/) (recommended for development)
- An active internet connection for API access

## Installation

1. Clone the repository:

    ```sh
    git clone https://github.com/fairyofshampoo/CycleCareDesktop.git
    ```

2. Open the solution file `CycleCare.sln` in Visual Studio.

3. Restore the NuGet packages:

    ```sh
    dotnet restore
    ```

## Usage

1. Build the application in Visual Studio by selecting `Build > Build Solution`.

2. Run the application by selecting `Debug > Start Debugging` or pressing `F5`.

3. Sign up or sign in to start using the application.

## Configuration

### API Endpoint

The application requires the endpoint of the REST API it connects to. You can configure this endpoint in the `resources.resx` file:

```resx
BASE_URL = localhost:3000
