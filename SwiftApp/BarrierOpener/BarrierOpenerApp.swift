//
//  BarrierOpenerApp.swift
//  BarrierOpener
//
//  Created by Евгений Петрашко on 06.04.2024.
//

import SwiftUI
import FirebaseCore
import FirebaseFirestore
import FirebaseAuth


class AppDelegate: NSObject, UIApplicationDelegate {
  func application(_ application: UIApplication,
                   didFinishLaunchingWithOptions launchOptions: [UIApplication.LaunchOptionsKey : Any]? = nil) -> Bool {
    FirebaseApp.configure()
      Auth.auth().signIn(withEmail: "clientbarrieropener@gmail.com", password: "") { authResult, error in
          if let error = error{
              print(error.localizedDescription)
          }
      }

    return true
  }
}

@main
struct BarrierOpenerApp: App {
    @UIApplicationDelegateAdaptor(AppDelegate.self) var delegate
    
    var body: some Scene {
        WindowGroup {
            ContentView()
        }
    }
}
