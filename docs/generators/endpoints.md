---
title: Endpoints
layout: default
parent: Generators
nav_order: 2
---

# Endpoint Generators

All endpoint generators produce **per-entity** files using FastEndpoints.

## CreateEndpointGenerator

Generates a `POST /{entities}` endpoint that accepts a create request, maps it to the domain entity, persists it, and returns the created resource.

## GetAllEndpointGenerator

Generates a `GET /{entities}` endpoint with built-in pagination support (`?page=1&pageSize=10`). Returns a paginated response with `TotalCount` metadata.

## GetEndpointGenerator

Generates a `GET /{entities}/{id}` endpoint with dynamic route constraints based on key type (`:guid`, `:int`, etc.). Returns 404 when the entity is not found.

## UpdateEndpointGenerator

Generates a `PUT /{entities}/{id}` endpoint that accepts an update request, validates it, and persists the changes.

## DeleteEndpointGenerator

Generates a `DELETE /{entities}/{id}` endpoint that removes the entity and returns appropriate status codes.

## Generated HTTP Endpoints

For an entity called `Product`:

```
POST   /products              Create a new product
GET    /products              List all products (paginated)
GET    /products/{id:guid}    Get a single product
PUT    /products/{id:guid}    Update a product
DELETE /products/{id:guid}    Delete a product
```
