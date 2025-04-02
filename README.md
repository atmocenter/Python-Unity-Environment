# XR Python Coding Environment — Public Version

This Unity-based XR environment allows users to learn and practice Python coding in a virtual world. It supports multiplayer functionality, Python execution via Docker, and Moodle integration for course content and login features.

---

## 🛠 Requirements

To run this project locally, you will need:

- **Unity Editor** version `2022.3.7f1`  
  > Other versions have not been tested and may not work correctly.

- **Docker Desktop**  
  > Docker is used to run Python scripts in isolated environments. Ensure the Python image is installed.

- **Moodle Instance**  
  > Moodle must be set up with API access enabled.

---

## 🧱 Moodle Database Setup

Manually create the following table in your Moodle database to store API WebTokens linked to Moodle user accounts.

- **Database Name:** `unity`
- **Table Name:** `xr_webtokens`
- **Table Fields:**
  - `id` (int, primary key)
  - `mdl45_user_id` (int, Moodle user ID)
  - `webtoken` (string, required)
  - `timecreated` (timestamp)
  - `lastip` (string)
  - `lastaccess` (timestamp)

This table is required for the login and authentication system in the XR environment.

---

## 🚀 Installing the Unity Project

1. **Clone the repository** to your local machine:

   ```bash
   git clone https://github.com/your-username/your-repo-name.git
   ```

2. **Open the project** in Unity Hub.  
   Make sure you have Unity version `2022.3.7f1` installed.

3. **Install the following Unity packages (if not already installed):**

   - `TextMeshPro` *(Unity built-in)*
   - `NuGetForUnity`  
     [→ NuGetForUnity Repository](***insert link here***)
   - `ParrelSync`  
     [→ ParrelSync Repository](***insert link here***)

---

## 🎮 Unity Assets Used

These Unity assets are included in the project:

- **Mirror** — for multiplayer networking  
- **Joystick Pack** — for mobile/joystick support

---

## 🏗 Setting Up Scene Objects

### 1. `MyNetworkManager` (Network Control Object)

This object handles all multiplayer network operations.

- **Components to Configure:**

  - **Network Manager**
    - `Network Address`: Set this to the server IP or domain.
    - `Transport`: Uses `Simple Web Transport` (from Mirror).

  - **Network Manager HUD** *(development only)*
    - Allows you to start/stop the server or connect as a client.
    - ⚠️ Should be disabled or removed before deployment.

  - **Simple Web Transport**
    - `Port`: Set the port used for WebSocket communication. Make sure it’s available on your server.

---

### 2. `QuizBoard` (child of `LMS Connection`)

- In the `HTTP Request` component:
  - **Moodle API URL**: Set this to your Moodle API endpoint.
  - **Course ID**: Set the corresponding Moodle Course ID.

---

### 3. `Login` Object (child of `"Login or Sign Up"`)

- Configure the `Authentication` script component:

  - `Db_server`: Your database server domain or IP.
  - `Db_name`: The database name (`unity`).
  - `Db_user`: Database user with access.
  - `Db_password`: Password for the above user.
  - `Db_table`: Table storing user credentials (e.g., `xr_webtokens`).

---

## 🧪 Running the Project

1. Start **Docker Desktop** and ensure it is running.
2. Open the Unity project and enter Play Mode.
3. You can now test login, Python scripting, and LMS integration locally.

---

## 📚 License

This project is open-source and available under the MIT License.

---

## 🙋 Support

For questions or contributions, please open an issue or pull request on GitHub.
