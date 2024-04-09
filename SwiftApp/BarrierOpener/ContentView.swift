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
    
    var body: some View {
        VStack {
            Button() {
                self._buttonDisable.toggle()
                
                OppenBarrierClick()
                
                self._buttonDisable.toggle()
            } label: {
                Text("OPEN")
                    .font(.system(size: 36))
                    .padding(.all, 20)
                    .foregroundStyle(.white)
                    .padding()
                    .background(Color.green)
                    .clipShape(Circle())
                    .overlay {
                        Circle().stroke(.white, lineWidth: 4)
                        }
                    .shadow(radius: 7)
            }
            .disabled(self._buttonDisable)
            
        }
        .padding()
    }
    
    func OppenBarrierClick() {
        let formatter = DateFormatter()
        formatter.dateFormat = "yyyy-MM-dd HH:mm:ss"
        let utcDate = NSDate() as Date
        let utcDateString = formatter.string(from: utcDate)
        
        self._db.child("action").child(NSUUID().uuidString).setValue(["SecretKey": "1e7e5e8c-166b-411c-a16f-6bca38cf8dd6", "RequestDateTimeUts": utcDateString, "DeviceName": UIDevice.current.model])
    }
}

#Preview {
    ContentView()
}
