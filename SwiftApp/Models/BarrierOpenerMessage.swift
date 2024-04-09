//
//  BarrierOpenerMessage.swift
//  BarrierOpener
//
//  Created by Евгений Петрашко on 08.04.2024.
//

import Foundation

struct BarrierOpenerMessage: Codable {
    var SecretKey: UUID
    var RequestDateTimeUts: String
    var DeviceName: String
}
