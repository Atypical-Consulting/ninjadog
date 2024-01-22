// Copyright (c) 2020-2024 Atypical Consulting SRL. All rights reserved.
// Atypical Consulting SRL licenses this file to you under the Proprietary license.
// See the LICENSE file in the project root for full license information.

namespace Ninjadog.Engine.Core.Models;

/// <summary>
/// Represents the different types of entity relationships in a database.
/// </summary>
public enum NinjadogEntityRelationshipType
{
    /// <summary>
    /// A one-to-one relationship where each record in one table corresponds to one record in another table.
    /// </summary>
    OneToOne,

    /// <summary>
    /// A one-to-many relationship where a single record in one table relates to multiple records in another table.
    /// </summary>
    OneToMany,

    /// <summary>
    /// A many-to-many relationship where multiple records in one table relate to multiple records in another table.
    /// </summary>
    ManyToMany,

    /// <summary>
    /// A one-way relationship where a record in one table points to a record in another table without a reciprocal link.
    /// </summary>
    OneWay,

    /// <summary>
    /// A many-way relationship where a record in one table points to multiple records in another table without reciprocal links.
    /// </summary>
    ManyWay
}
