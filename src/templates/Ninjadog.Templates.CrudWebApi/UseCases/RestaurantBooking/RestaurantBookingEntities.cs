// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

namespace Ninjadog.Templates.CrudWebAPI.UseCases.RestaurantBooking;

/// <summary>
/// Defines the entities specific to the RestaurantBooking use case.
/// </summary>
public sealed class RestaurantBookingEntities : NinjadogEntities
{
    // https://hevodata.com/learn/schema-example/#Schema_Example_Restaurant_Booking
    private const string Customer = "Customer";
    private const string Booking = "Booking";
    private const string Table = "Table";
    private const string Order = "Order";
    private const string OrderMenuItem = "OrderMenuItem";
    private const string Menu = "Menu";
    private const string MenuItem = "MenuItem";
    private const string MenuItemIngredient = "MenuItemIngredient";
    private const string Ingredient = "Ingredient";
    private const string IngredientType = "IngredientType";
    private const string Staff = "Staff";
    private const string StaffRole = "StaffRole";

    /// <summary>
    /// Initializes a new instance of the <see cref="RestaurantBookingEntities"/> class.
    /// </summary>
    public RestaurantBookingEntities()
    {
        Add(Customer, new NinjadogEntity(
            new NinjadogEntityProperties
            {
                { "CustomerId", new NinjadogEntityId() },
                { "CustomerFirstName", new NinjadogEntityProperty<string>() },
                { "CustomerSurname", new NinjadogEntityProperty<string>() },
                { "PhoneNumber", new NinjadogEntityProperty<string>() },
                { "CellphoneNumber", new NinjadogEntityProperty<string>() },
                { "EmailAddress", new NinjadogEntityProperty<string>() },
                { "OtherCustomerDetails", new NinjadogEntityProperty<string>() },

                // { "Items", new NinjadogEntityRelationship(Booking, NinjadogEntityRelationshipType.OneToMany) }
            }));

        Add(Booking, new NinjadogEntity(
            new NinjadogEntityProperties
            {
                { "BookingId", new NinjadogEntityId() },
                { "DateOfBooking", new NinjadogEntityProperty<DateTime>() },
                { "NumberInParty", new NinjadogEntityProperty<int>() },
            }));

        Add(Table, new NinjadogEntity(
            new NinjadogEntityProperties
            {
                { "TableNumber", new NinjadogEntityProperty<int>(true) },
                { "TableDetails", new NinjadogEntityProperty<string>() },
            }));

        Add(Order, new NinjadogEntity(
            new NinjadogEntityProperties
            {
                { "OrderId", new NinjadogEntityId() },
                { "OrderDateTime", new NinjadogEntityProperty<DateTime>() },
            }));

        Add(OrderMenuItem, new NinjadogEntity(
            new NinjadogEntityProperties
            {
                { "OrderMenuItemId", new NinjadogEntityId() },
                { "OrderMenuItemQuantity", new NinjadogEntityProperty<int>() },
                { "OrderMenuItemComments", new NinjadogEntityProperty<string>() },
            }));

        Add(Menu, new NinjadogEntity(
            new NinjadogEntityProperties
            {
                { "MenuId", new NinjadogEntityId() },
                { "MenuDate", new NinjadogEntityProperty<DateTime>() },
            }));

        Add(MenuItem, new NinjadogEntity(
            new NinjadogEntityProperties
            {
                { "MenuItemId", new NinjadogEntityId() },
                { "MenuItemDescription", new NinjadogEntityProperty<string>() },
                { "MenuItemPrice", new NinjadogEntityProperty<decimal>() },
            }));

        Add(MenuItemIngredient, new NinjadogEntity(
            new NinjadogEntityProperties
            {
                { "MenuItemIngredientId", new NinjadogEntityId() },
                { "ItemQuantity", new NinjadogEntityProperty<int>() },
            }));

        Add(Ingredient, new NinjadogEntity(
            new NinjadogEntityProperties
            {
                { "IngredientId", new NinjadogEntityId() },
                { "IngredientName", new NinjadogEntityProperty<string>() },
            }));

        Add(IngredientType, new NinjadogEntity(
            new NinjadogEntityProperties
            {
                { "IngredientTypeCode", new NinjadogEntityId() },
                { "IngredientTypeDescription", new NinjadogEntityProperty<string>() },
            }));

        Add(Staff, new NinjadogEntity(
            new NinjadogEntityProperties
            {
                { "StaffId", new NinjadogEntityId() },
                { "StaffFirstName", new NinjadogEntityProperty<string>() },
                { "StaffLastName", new NinjadogEntityProperty<string>() },
            }));

        Add(StaffRole, new NinjadogEntity(
            new NinjadogEntityProperties
            {
                { "StaffRoleCode", new NinjadogEntityId() },
                { "StaffRoleDescription", new NinjadogEntityProperty<string>() },
            }));
    }
}
