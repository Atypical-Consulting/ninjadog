// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

namespace Ninjadog.Templates.CrudWebAPI.UseCases.RestaurantBooking;

/// <summary>
/// Provides the specific configuration for the "RestaurantBooking" application.
/// This sealed record inherits from NinjadogConfiguration and sets predefined values
/// tailored for the RestaurantBooking project, such as its name, version, description, and paths.
/// </summary>
public sealed record RestaurantBookingConfiguration()
    : NinjadogConfiguration(
        Name: "RestaurantBooking",
        Version: "1.0.0",
        Description: "A application to manage restaurant bookings.",
        RootNamespace: "RestaurantBooking.CrudWebApi",
        OutputPath: "src/applications/RestaurantBooking");
