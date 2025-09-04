# FoodFacts: A .NET MAUI App

![foodfacts_maui_app_gif](https://github.com/user-attachments/assets/b083d1f5-ce2d-4ffc-a683-5b725f05b300)

## 📝 Project Description

FoodFacts is a cross-platform mobile application for Android built with .NET MAUI. 
Using the Open Food Facts API, it allows users to search for food products, view nutrition details 
and ingredient information. It also saves their favorite products to a local on-device database.

It features a clean MVVM architecture, dependency injection, and demonstrates advanced capabilities
such as calling native Android APIs for actionable notifications and persisting data for offline use with SQLite.

## 🌟 Technical Features & Skills Showcased

This project was built to demonstrate a comprehensive set of modern, professional mobile development practices.

* **Architecture (MVVM & DI):** Built on a clean, testable MVVM architecture with a decoupled UI and logic. Utilizes .NET MAUI's built-in Dependency Injection container to manage services and ViewModels.
* **Modern Navigation:** Uses .NET MAUI Shell for a robust, modern navigation structure, including a tab bar and URI-based page navigation.
* **REST API Consumption:** Asynchronously fetches live data from the Open Food Facts API using `HttpClient`, with proper JSON deserialization into C# models.
* **Native Android Integration:** Demonstrates the ability to go beyond the cross-platform layer by implementing rich, actionable notifications that call directly into native Android APIs (`NotificationManagerCompat`, `PendingIntent`).
* **Data Persistence (SQLite):** Implements a local SQLite database to allow users to save "Favorite" products, demonstrating skills in offline data storage.
* **Persistent User Settings:** Uses the `.NET MAUI` `Preferences` API to save and load user settings, such as a custom username and the app's theme (Light/Dark).
* **Dynamic & Responsive UI:** The UI is built with XAML and features a responsive two-column grid (`GridItemsLayout`), data-driven UI changes (`DataTriggers`), and user feedback controls (`ActivityIndicator`).
* **Advanced Data Manipulation:** Implements on-the-fly sorting of data collections using C# and LINQ.

## 🛠️ Tech Stack

* .NET MAUI (.NET 8)
* C# & XAML
* MVVM Community Toolkit
* SQLite-net-pcl
* Native Android APIs
* Open Food Facts REST API

## 💻 Setup & Installation

1.  Clone the repository: `git clone`
2.  Open the `FoodFacts.sln` file in Visual Studio 2022 (v17.8 or newer).
3.  Restore the NuGet packages (this should happen automatically).
4.  Connect an Android device or start an Android emulator.
5.  Set the `FoodFacts` project as the startup project and run it.

## Future Work

* **Implement Unit Tests:** Build out a suite of unit tests for the ViewModels using a framework like xUnit to ensure long-term code quality and reliability.
* **Add iOS Native Features:** Implement the native notification feature for iOS using `UNUserNotificationCenter` to achieve feature parity with the Android version.
