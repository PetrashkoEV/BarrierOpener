//
//  ContentView.swift
//  BarrierOpener
//
//  Created by Евгений Петрашко on 06.04.2024.
//

import SwiftUI
import FirebaseDatabase
import FirebaseAuth

struct ContentView: View {
    
    @State private var _db: DatabaseReference! = Database.database().reference()
    @State private var _buttonDisable: Bool = false
    @State private var _buttonColor = Color.green
    
    var body: some View {
        VStack {
                Button() {
                    _buttonDisable.toggle()
                    _buttonColor = Color.gray
                    
                    Task{
                        OppenBarrierClickTask()
                        
                        try await Task.sleep(nanoseconds: UInt64(10 * Double(NSEC_PER_SEC)))
                        
                        _buttonDisable.toggle()
                        _buttonColor = Color.green
                    }
                    
                } label: {
                    Text("OPEN")
                        .font(.system(size: 36))
                        .padding(.all, 20)
                        .foregroundStyle(.white)
                        .padding()
                        .background(_buttonColor)
                        .clipShape(Circle())
                        .overlay {
                            Circle().stroke(.white, lineWidth: 4)
                        }
                        .shadow(radius: 7)
                }
                .disabled(_buttonDisable)
            }
        .padding()
    }
    
    func OppenBarrierClickTask() {
        let utcDateString = getCurrentUTCdate()
        
        _db.child("action").child(NSUUID().uuidString).setValue(["RequestDateTime": utcDateString, "DeviceName": UIDevice.current.model])
    }
    
    func getCurrentUTCdate() -> String{
        let currentDate = Date()
        let formater = DateFormatter()
        formater.timeZone = TimeZone(abbreviation: "UTC")
        formater.dateFormat = "yyyy-MM-dd HH:mm:ss"
        return formater.string(from: currentDate)
    }
}

#Preview {
    ContentView()
}
