// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

namespace Ninjadog.Templates.CrudWebAPI.UseCases.RestaurantBooking;

/// <summary>
/// Represents the settings for the RestaurantBooking use case.
/// </summary>
public record RestaurantBookingSettings()
    : NinjadogSettings(
        new RestaurantBookingConfiguration(),
        new RestaurantBookingEntities());
