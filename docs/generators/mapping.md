---
title: Mapping
layout: default
parent: Generators
nav_order: 5
---

# Mapping Generators

All mapping generators produce **single files** containing extension methods for all entities.

## ApiContractToDomainMapperGenerator

Maps API contract (request) objects to domain entities. Used by Create and Update endpoints.

## DomainToApiContractMapperGenerator

Maps domain entities to API contract (response) objects. Used by all read endpoints.

## DomainToDtoMapperGenerator

Maps domain entities to DTOs. Used internally by services and repositories.

## DtoToDomainMapperGenerator

Maps DTOs back to domain entities. Used when reading from the database layer.
