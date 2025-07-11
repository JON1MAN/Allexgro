
# Allexgro
Allexgro is my lightweight vision of [Allegro](https://allegro.pl/) 

## Table of Contents

- [Allexgro](#allexgro)
  - [Stack](#stack)
  - [How to launch application](#how-to-launch-application)

- [API](#api)
  - [RegistrationController](#registrationcontroller)
    - [`POST /api/v1/registration`](#post-apiv1registration)
  - [AuthorizationController](#authorizationcontroller)
    - [`POST /api/v1/authorization`](#post-apiv1authorization)
  - [ProductController](#productcontroller)
    - [`GET /api/v1/product/{productId}`](#get-apiv1productproductid)
    - [`GET /api/v1/product`](#get-apiv1product)
    - [`GET /api/v1/product/my`](#get-apiv1productmy)
    - [`POST /api/v1/product`](#post-apiv1product)
    - [`GET /api/v1/product/product-categories`](#get-apiv1productproduct-categories)
    - [`GET /api/v1/product/product-categories/{categoryId}/product-types`](#get-apiv1productproduct-categoriescategoryidproduct-types)
    - [`GET /api/v1/product/product-categories/product-types/{typeId}/attributes`](#get-apiv1productproduct-categoriesproduct-typestypeidattributes)
    - [`POST /api/v1/search`](#post-apiv1search)
  - [UserController](#usercontroller)
    - [`GET /api/v1/user/me`](#get-apiv1userme)
    - [`PUT /api/v1/user`](#put-apiv1user)
  - [CheckoutController](#checkoutcontroller)
    - [`PUT /api/v1/checkout`](#put-apiv1checkout)

- [Resources](#resources-that-helped-me-in-building-this-project)

## Stack:
- backend - **C#** ASP .NET 9.0.201
- database - **PostgreSQL** 17.4
- frontend - **Vite + React ts** (in separate repo: Allexgro Frontend)

## How to launch application:
1) in your terminal from project root: **`docker compose up -d`** - this will run container with postgreSQL
2) **`dotnet ef database update`** - to apply migrations from /Allexgro/Migrations to database
3)  `export $(xargs < .env)` - to import env variables to your os environment
3) **dotnet build** - to build a project (can throw warnings depends on your IDE)
4) **dotnet run** - run a project (port - **_localhost:5156_**)

# API
## RegistrationController

**POST** `/api/v1/registration`
**Request Body:**  (UserRegistrationDTO)
```json
{
	"email"  :  "holoraj1134@ethsms.com",
	"password"  :  "password123A_"
}
```

### Parameters Description

| **Field**   |  **Type**  |                                                             **Description** |
|:------------|:----------:|------------------------------------------------------------------------------:|
| `email`     | `string`   | Unique user email that you want to register with.                            |
| `password`  | `string`   | Minimum 8 characters, must include a number, lowercase and uppercase letter, and a special character. |


Full *UserRegistrationDTO.cs*
```cs
public  class  UserRegistrationDTO
{
	public  string  Email { get; set; }
	public  string  Password { get; set; }
}
```

**Possible responses**:
`200 ok` - successful user registration
**Response body**: (Response)
```json
{

	"isSuccessful":  true,
	"body":  {

		"accessToken":  "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.....",
		"stripeOnboardingUrl": "https://connect.stripe.com/setup/..."
	},
	"message":  "User Registration Successfull!"

}
```

### Parameters Description
| **Field**                  |     **Type**     |                                                             **Description** |
|:---------------------------|:----------------:|------------------------------------------------------------------------------:|
| `isSuccessful`             | `bool`           | Indicates if the request was successfully processed.                         |
| `body`                     | `object`         | Wrapper object containing response data.                                     |
| `body.accessToken`         | `string`         | JWT token used for authenticating and authorizing future requests.           |
| `body.stripeOnboardingUrl` | `string?`        | URL for Stripe Express onboarding to link user’s bank info for payments.     |
| `message`                  | `string`         | Message string describing the result of the request.                         |


## AuthorizationController
**POST** `/api/v1/authorization`
**Request Body:**  (UserLoginDTO)
```json
{
	"email"  :  "test@gmail.com",
	"password"  :  "password123A_"
}
```

**Possible responses**:
`200 ok` - successful user registration
**Response body**: (Response)
```json
{

	"isSuccessful":  true,
	"body":  {

		"accessToken":  "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.....",
		"stripeOnboardingUrl": null
	},
	"message":  "Login Successfull!"

}
```

Parameters same as in registration.

`404 NOT FOUND`  - if there is no such user

## ProductController

**GET** `/api/v1/product/{productId}`
**PathVariable**:
`productId` - id of product that you want to get

**Possible responses**:
`200 ok` - product with provided id found
**Response body**: (ProductDTO)
```json
{
"id":  8,
"name":  "nokia 3310",
"price":  34,
"amount":  12,
"userId":  "user uuid",
"stripeProductId":  "stripe prod id",
"stripeProductPriceId":  "stripe price id",
"productCategory":  {
	"id":  1,
	"name":  "ELECTRONICS"
},
"productType":  {
	"id":  2,
	"name":  "SMARTPHONES",
	"productCategoryId":  1
},
"productAttributes":  [
	{
	"productTypeId":  2,
	"attributeKeyId":  6,
	"attributeKey":  {
	"id":  6,
	"name":  "BRAND",
	"dataType":  "STRING",
	"productTypeId":  2,
	"isPredefined":  true
	},
	"booleanValue":  false,
	"decimalValue":  0,
	"stringValue":  "Samsung"
	},
	{
	"productTypeId":  2,
	"attributeKeyId":  7,
	"attributeKey":  {
	"id":  7,
	"name":  "STORAGE_CAPACITY",
	"dataType":  "DECIMAL",
	"productTypeId":  2,
	"isPredefined":  false
	},
	"booleanValue":  false,
	"decimalValue":  1000,
	"stringValue":  ""
	},
	{
	"productTypeId":  2,
	"attributeKeyId":  8,
	"attributeKey":  {
	"id":  8,
	"name":  "HAS_5G",
	"dataType":  "BOOLEAN",
	"productTypeId":  2,
	"isPredefined":  false
	},
	"booleanValue":  true,
	"decimalValue":  0,
	"stringValue":  ""
	}
]
}
```
**Parameters description:**

| **Field**                |           **Type**            |                                                            **Description** |
|:-------------------------|:-----------------------------:|---------------------------------------------------------------------------:|
| `id`                    | `int`                         | ID of the product.                                                        |
| `name`                  | `string`                      | Product name.                                                             |
| `price`                 | `decimal`                     | Product price.                                                            |
| `amount`                | `int`                         | Available quantity of the product.                                        |
| `userId`                | `string`                      | ID of the user who created the product.                                   |
| `stripeProductId`       | `string?`                     | Stripe product ID linked to this product.                                 |
| `stripeProductPriceId`  | `string?`                     | Stripe price ID (used for Stripe.Checkout.SessionLineItemOptions).        |
| `productCategory`       | `ProductCategoryDTO`          | Product category containing its `id` and `name`.                          |
| `productType`           | `ProductTypeDTO`              | Product type with its `id`, `name`, and associated `productCategoryId`.   |
| `productAttributes`     | `List<ProductAttributeDTO>`   | Array of attributes (key-value structure) representing product features.  |



`404 NOT FOUND`  - if there is no such product with provided id


## **GET** `/api/v1/product`

Retrieves all products from the system.

### **Possible responses**

- `200 OK` – list of all products

### **Response body**: `List<ProductDTO>`
```json
[
  {
    "id": 8,
    "name": "nokia 3310",
    "price": 34,
    "amount": 12,
    "userId": "user uuid",
    "stripeProductId": "stripe prod id",
    "stripeProductPriceId": "stripe price id",
    "productCategory": {
      "id": 1,
      "name": "ELECTRONICS"
    },
    "productType": {
      "id": 2,
      "name": "SMARTPHONES",
      "productCategoryId": 1
    },
    "productAttributes": [
      {
        "productTypeId": 2,
        "attributeKeyId": 6,
        "attributeKey": {
          "id": 6,
          "name": "BRAND",
          "dataType": "STRING",
          "productTypeId": 2,
          "isPredefined": true
        },
        "booleanValue": false,
        "decimalValue": 0,
        "stringValue": "Samsung"
      },
      {
        "productTypeId": 2,
        "attributeKeyId": 7,
        "attributeKey": {
          "id": 7,
          "name": "STORAGE_CAPACITY",
          "dataType": "DECIMAL",
          "productTypeId": 2,
          "isPredefined": false
        },
        "booleanValue": false,
        "decimalValue": 1000,
        "stringValue": ""
      },
      {
        "productTypeId": 2,
        "attributeKeyId": 8,
        "attributeKey": {
          "id": 8,
          "name": "HAS_5G",
          "dataType": "BOOLEAN",
          "productTypeId": 2,
          "isPredefined": false
        },
        "booleanValue": true,
        "decimalValue": 0,
        "stringValue": ""
      }
    ]
  }
]
```

###  Parameters Description

| **Field**               |   **Type**   |                                             **Description** |
|:------------------------|:------------:| -----------------------------------------------------------:|
| `id`                    | `int`        | Unique identifier of the product.                           |
| `name`                  | `string`     | Name of the product.                                        |
| `price`                 | `decimal`    | Price of the product.                                       |
| `amount`                | `int`        | Quantity available in stock.                                |
| `userId`                | `string`     | UUID of the user who created the product.                   |
| `stripeProductId`       | `string?`    | ID of the product in Stripe (used for payments).            |
| `stripeProductPriceId`  | `string?`    | Price ID in Stripe, used for Stripe Checkout sessions.      |
| `productCategory`       | `ProductCategoryDTO` | Product category containing `id` and `name`.        |
| `productType`           | `ProductTypeDTO`     | Product type containing `id`, `name`, and category ID. |
| `productAttributes`     | `List<ProductAttributeDTO>` | List of key-value attributes for the product.        |


## **GET** `/api/v1/product/my`

Returns a list of products created by the currently authenticated user.

>  **Authorization required**  
> This endpoint requires a valid **Bearer JWT token** inside request header.

---

### Possible Responses

- **`200 OK`** – List of products created by the logged-in user.
- **`404 NOT FOUND`** – No products were found for the authenticated user.

---

### Response Body — `List<ProductDTO>`

```json
[
  {
    "id": 8,
    "name": "nokia 3310",
    "price": 34,
    "amount": 12,
    "userId": "user uuid",
    "stripeProductId": "stripe prod id",
    "stripeProductPriceId": "stripe price id",
    "productCategory": {
      "id": 1,
      "name": "ELECTRONICS"
    },
    "productType": {
      "id": 2,
      "name": "SMARTPHONES",
      "productCategoryId": 1
    },
    "productAttributes": [
      {
        "productTypeId": 2,
        "attributeKeyId": 6,
        "attributeKey": {
          "id": 6,
          "name": "BRAND",
          "dataType": "STRING",
          "productTypeId": 2,
          "isPredefined": true
        },
        "booleanValue": false,
        "decimalValue": 0,
        "stringValue": "Samsung"
      }
    ]
  }
]
```

parameters same as before.

## **POST** `/api/v1/product`

Creates a new product for the currently authenticated user.

**Authorization required:** Bearer token

---

### Request Body — `ProductCreateDTO`

```json
{
  "name": "nokia 3310",
  "price": 34.0,
  "amount": 12,
  "productCategoryId": 1,
  "productTypeId": 2,
  "productAttributes": [
    {
      "productTypeId": 2,
      "attributeKeyId": 6,
      "attributeKey": {
        "id": 6,
        "name": "BRAND",
        "dataType": "STRING",
        "productTypeId": 2,
        "isPredefined": true
      },
      "booleanValue": false,
      "decimalValue": 0,
      "stringValue": "Samsung"
    }
  ]
}
```

### Possible Responses

-   **`200 OK`** – Product successfully created. Returns the created `ProductDTO`.
-   **`401 UNAUTHORIZED`** – User is not authenticated.
-   **`400 BAD REQUEST`** – Input data is invalid or incomplete.

### Response Body — `ProductDTO`


## **GET** `/api/v1/product/product-categories`

Returns a list of all available product categories.

**Authorization required:** Bearer token

---

### Possible Responses

- **`200 OK`** – Returns a list of product categories.
- **`404 NOT FOUND`** – No product categories found.

---

### Response Body — `List<ProductCategory>`

```json
[
  {
    "id": 1,
    "name": "ELECTRONICS"
  },
  {
    "id": 2,
    "name": "FASHION"
  }
]
```

## **GET** `/api/v1/product/product-categories/{categoryId}/product-types`

Returns all product types that belong to the specified product category.

**Authorization required:** Bearer token

---

### Path Variable

- `categoryId` – ID of the product category whose types should be returned.

---

### Possible Responses

- **`200 OK`** – Product types for the given category found and returned.
- **`404 NOT FOUND`** – No product types found for the provided category ID.

---

### Response Body — `List<ProductType>`

```json
[
  {
    "id": 2,
    "name": "SMARTPHONES",
    "productCategoryId": 1,
    "productCategory": {
      "id": 1,
      "name": "ELECTRONICS"
    },
    "attributeKeys": [
      {
        "id": 6,
        "name": "BRAND",
        "dataType": "STRING",
        "productTypeId": 2,
        "isPredefined": true,
        "allowedValues": ["Samsung", "Apple", "Nokia"]
      },
      {
        "id": 7,
        "name": "STORAGE_CAPACITY",
        "dataType": "DECIMAL",
        "productTypeId": 2,
        "isPredefined": false,
        "allowedValues": null
      }
    ]
  }
]
```

### Parameters Description

| **Field**               |           **Type**            |                                            **Description** |
|:------------------------|:-----------------------------:| ----------------------------------------------------------:|
| `id`                    | `int`                         | Unique identifier of the product type.                     |
| `name`                  | `string`                      | Name of the product type.                                  |
| `productCategoryId`     | `int`                         | ID of the category this type is associated with.           |
| `productCategory`       | `ProductCategory`             | Object representing the category with `id` and `name`.     |
| `attributeKeys`         | `List<ProductAttributeKey>`   | List of attribute keys associated with this product type.  |

### Attribute Key Description (`ProductAttributeKey`)
| **Field**        |         **Type**         |                                      **Description** |
|:-----------------|:------------------------:| ----------------------------------------------------:|
| `id`             | `int`                    | Unique identifier of the attribute key.              |
| `name`           | `string`                 | Name of the attribute.                               |
| `dataType`       | `string`                 | Data type of the attribute (e.g. STRING, DECIMAL, BOOLEAN).   |
| `productTypeId`  | `int`                    | ID of the product type this attribute belongs to.    |
| `productType`    | `ProductType`            | Parent product type reference.                       |
| `isPredefined`   | `bool`                   | Indicates whether the attribute uses predefined values. |
| `allowedValues`  | `List<string>?`          | Optional list of predefined values for this key.     |

## **GET** `/api/v1/product/product-categories/product-types/{typeId}/attributes`

Returns all attribute keys available for a given product type.

**Authorization required:** Bearer token

---

### Path Variable

- `typeId` – ID of the product type whose attribute keys should be returned.

---

### Possible Responses

- **`200 OK`** – Attribute keys for the given product type returned successfully.
- **`404 NOT FOUND`** – No attribute keys found for the provided type ID.

---

### Response Body — `List<ProductAttributeKey>`

```json
[
  {
    "id": 6,
    "name": "BRAND",
    "dataType": "STRING",
    "productTypeId": 2,
    "productType": {
      "id": 2,
      "name": "SMARTPHONES",
      "productCategoryId": 1,
      "productCategory": {
        "id": 1,
        "name": "ELECTRONICS"
      },
      "attributeKeys": []
    },
    "isPredefined": true,
    "allowedValues": ["Samsung", "Apple", "Nokia"]
  },
  {
    "id": 7,
    "name": "STORAGE_CAPACITY",
    "dataType": "DECIMAL",
    "productTypeId": 2,
    "productType": { ... },
    "isPredefined": false,
    "allowedValues": null
  }
]
```

### Parameters Description
| **Field**        |         **Type**         |                                      **Description** |
|:-----------------|:------------------------:| ----------------------------------------------------:|
| `id`             | `int`                    | Unique identifier of the attribute key.              |
| `name`           | `string`                 | Name of the attribute.                               |
| `dataType`       | `string`                 | Data type of the attribute (e.g. STRING, DECIMAL).   |
| `productTypeId`  | `int`                    | ID of the product type this attribute belongs to.    |
| `productType`    | `ProductType`            | Parent product type reference.                       |
| `isPredefined`   | `bool`                   | Indicates whether the attribute uses predefined values. |
| `allowedValues`  | `List<string>?`          | Optional list of predefined values for this key.     |

## ProductController

## **POST** `/api/v1/search`

Filters products based on provided criteria such as price range, category, type, and product attributes.

---

### Request Body — `FilterDTO`

```json
{
  "minPrice": 100.00,
  "maxPrice": 500.00,
  "productCategoryId": 1,
  "productTypeId": 2,
  "productAttributes": [
    {
      "productTypeId": 2,
      "attributeKeyId": 6,
      "attributeKey": {
        "id": 6,
        "name": "BRAND",
        "dataType": "STRING",
        "productTypeId": 2,
        "isPredefined": true
      },
      "booleanValue": false,
      "decimalValue": 0,
      "stringValue": "Samsung"
    }
  ]
}
```

### Possible Responses

-   **`200 OK`** – Returns a list of filtered products.

### Response Body — `List<ProductDTO>`

_Same structure as in `/api/v1/product` and other product-related endpoints._

| **Field**               |               **Type**               |                                               **Description** |
|:------------------------|:------------------------------------:| --------------------------------------------------------------:|
| `minPrice`              | `decimal?`                           | Minimum price to filter by (optional).                         |
| `maxPrice`              | `decimal?`                           | Maximum price to filter by (optional).                         |
| `productCategoryId`     | `int?`                               | ID of the product category to filter by (optional).            |
| `productTypeId`         | `int?`                               | ID of the product type to filter by (optional).                |
| `productAttributes`     | `List<ProductAttributeDTO>?`         | List of attribute key-value pairs to match product attributes. |


## **GET** `/api/v1/user/me`

Returns profile information of the currently authenticated user.

**Authorization required:** Bearer token

---

### Possible Responses

- **`200 OK`** – User profile successfully retrieved.
- **`404 NOT FOUND`** – User profile not found (invalid token or user no longer exists).

---

### Response Body — `UserEditDTO`

```json
{
  "email": "user@example.com",
  "description": "I'm a passionate tech seller."
}
```

## Parameters Description
| **Field**     |   **Type**   |                       **Description** |
|:--------------|:------------:| --------------------------------------:|
| `email`       | `string`     | Email address of the user.             |
| `description` | `string?`    | Optional profile description.          |

## **PUT** `/api/v1/user`

Updates the profile information of the currently authenticated user.

**Authorization required:** Bearer token

---

### Request Body — `UserEditDTO`

```json
{
  "email": "user@example.com",
  "description": "Updated user description"
}
```

### Possible Responses

-   **`200 OK`** – User profile updated successfully. Returns the updated user data.
-   **`404 NOT FOUND`** – User not found or update failed.

### Response Body — `UserDTO`
```json
{
  "id": "user-uuid",
  "email": "user@example.com",
  "createdAt": "2024-01-05T14:22:31Z",
  "updateStripeAccountLinkUrl": "https://dashboard.stripe.com/onboarding/link",
  "description": "Updated user description"
}

```

| **Field**                     |       **Type**       |                                             **Description** |
|:------------------------------|:--------------------:| ------------------------------------------------------------:|
| `id`                          | `string`             | Unique identifier of the user.                               |
| `email`                       | `string`             | Updated email address of the user.                           |
| `createdAt`                   | `DateTime`           | Timestamp when the user account was created.                 |
| `updateStripeAccountLinkUrl` | `string`             | Stripe URL to update or complete account setup.              |
| `description`                 | `string?`            | Optional updated profile description.                        |


# CheckoutController
## **PUT** `/api/v1/checkout`

Creates a Stripe Checkout Session for the currently authenticated user based on the provided shopping cart.

**Authorization required:** Bearer token

---

### Request Body — `ShoppingCart`

```json
{
  "products": [
    {
      "id": 8,
      "name": "nokia 3310",
      "price": 34,
      "amount": 1,
      "userId": "user-uuid",
      "stripeProductId": "stripe-prod-id",
      "stripeProductPriceId": "stripe-price-id",
      "productCategory": {
        "id": 1,
        "name": "ELECTRONICS"
      },
      "productType": {
        "id": 2,
        "name": "SMARTPHONES",
        "productCategoryId": 1
      },
      "productAttributes": []
    }
  ]
}
```

### Possible Responses

-   **`200 OK`** – Stripe Checkout Session successfully created. Returns the session URL.
    
-   **`401 UNAUTHORIZED`** – User is not authenticated.
    
-   **`400 BAD REQUEST`** – Invalid or malformed shopping cart data.
    
### Response Body — `CheckoutSessionResponseUrlDTO`
```json
{
  "checkoutSessionResponseUrl": "https://checkout.stripe.com/pay/cs_test_abc123"
}
```

### Parameters Description — `CheckoutSessionResponseUrlDTO`
| **Field**                    |    **Type**    |                              **Description** |
|:-----------------------------|:--------------:| ---------------------------------------------:|
| `checkoutSessionResponseUrl`| `string`       | URL directing the user to the Stripe checkout session. |

### Parameters Description — `ShoppingCart`
| **Field**   |        **Type**        |                            **Description** |
|:------------|:----------------------:| ------------------------------------------:|
| `products`  | `List<ProductDTO>`     | List of products the user intends to buy.  |


### Resources that helped me in building this project:
https://www.nuget.org/packages/Npgsql.DependencyInjection \
https://developers.redhat.com/articles/2024/01/11/connect-dotnet-app-external-postgresql-database#setting_up__net_data_access_for_postgresql \
https://stackoverflow.com/questions/53724202/asp-net-core-httpsredirectionmiddleware-failed-to-determine-the-https-port-for-r \
https://tailwindcss.com/docs/installation/using-vite \
https://learn.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-9.0 \
https://www.reddit.com/r/dotnet/comments/1barp0b/starting_to_really_hate_the_way_aspnet_doesnt_cors/ \
https://learn.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-8.0#uc1 \
https://gondi-sai.medium.com/building-nested-components-with-child-routes-in-react-react-no-14-c9152db9f8cc \
https://sajadshafi.com/blog/dotnet-webapi-user-registration-login \
https://www.nuget.org/packages/Microsoft.AspNetCore.Identity.EntityFrameworkCore \
https://medium.com/@sajadshafi/jwt-authentication-in-c-net-core-7-web-api-b825b3aee11d \
https://learn.microsoft.com/en-us/aspnet/core/security/authentication/configure-jwt-bearer-authentication?view=aspnetcore-9.0 \
https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer \
https://www.nuget.org/packages/Microsoft.AspNetCore.Identity \
https://jwt.io/ \
https://www.npgsql.org/doc/types/datetime.html \
https://learn.microsoft.com/pl-pl/dotnet/api/system.datetime.touniversaltime?view=net-9.0 \
https://blog.wildermuth.com/2018/04/10/Using-JwtBearer-Authentication-in-an-API-only-ASP-NET-Core-Project/ \
https://dribbble.com/shots/26005535-Smarter-financial-Landing-Page \
https://liquid-glass-eta.vercel.app/ \
https://codesandbox.io/p/sandbox/iphone-react-component-forked-34jnzl \
https://dev.to/kevinbism/recreating-apples-liquid-glass-effect-with-pure-css-3gpl \
https://www.pexels.com/video/explosion-of-white-smoke-in-dark-background-7566041/ \
https://medium.com/@ravipatel.it/managing-configuration-and-environment-variables-in-net-b1c10d69d3d2 

**stripe** \
https://www.nuget.org/packages/Stripe.net/ \
https://docs.stripe.com/ \
https://docs.stripe.com/payments \
https://docs.stripe.com/development \
https://docs.stripe.com/connect \
https://docs.stripe.com/workflows \
https://docs.stripe.com/connect/testing#payouts \
https://docs.stripe.com/testing?architecture-style=resources&testing-method=card-numbers \
https://docs.stripe.com/api/account_links/create?lang=dotnet \
https://stackoverflow.com/questions/77749876/stripe-connect-express-test-account-not-updating-company-address \
https://docs.stripe.com/api/accounts/login_link/object?lang=dotnet \
https://docs.stripe.com/payments/checkout \
https://docs.stripe.com/api/checkout/sessions \
https://docs.stripe.com/checkout/quickstart?client=react \
https://docs.stripe.com/payments/accept-a-payment?platform=web&ui=stripe-hosted&shell=true&api=true#create-product-prices-upfront \
https://docs.stripe.com/api/checkout/sessions/create \
https://docs.stripe.com/connect/charges \
https://docs.stripe.com/api/prices/create 
https://docs.stripe.com/api/products/object \
https://stackoverflow.com/questions/46183171/how-to-add-custom-header-to-asp-net-core-web-api-response \
https://docs.stripe.com/testing \

