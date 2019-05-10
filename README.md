# GPS-Coordinates-Info
Service getting information GPS coordinates
An example of using web API with microservice template elements and information about file storage.
Target: takes a pair of GPS coordinates calculates the distance between them and finds their address.

Use
Start service.

Send

POST */api/coordinate

[
  { "x": 30.315868, "y": 59.939095 },
  { "x": 59.939095, "y": 59.939095 },
  { "x": 31.269915, "y": 58.522810 },
  { "x": -0.127660, "y": 51.507351 }
]

GET */api/coordinate

[
    {
        "idCoordinate": 1,
        "addressCounry": "Россия",
        "addressCity": " Санкт-Петербург",
        "addressRegiin": " Дворцовая площадь",
        "status": true,
        "x": 30.3158684,
        "y": 59.9390945
    },
    {
        "idCoordinate": 2,
        "addressCounry": "Россия",
        "addressCity": " Свердловская область",
        "addressRegiin": " болото Лих",
        "status": true,
        "x": 59.9390945,
        "y": 59.9390945
    },
    {
        "idCoordinate": 3,
        "addressCounry": "Россия",
        "addressCity": " Р-56",
        "addressRegiin": " Р-56",
        "status": true,
        "x": 31.2699146,
        "y": 58.52281
    },
    {
        "idCoordinate": 4,
        "addressCounry": "Великобритания",
        "addressCity": " Лондон",
        "addressRegiin": " Whitehall (Уайтхолл)",
        "status": true,
        "x": -0.12766,
        "y": 51.50735
    }
]

JSON File:

[
  {
    "distance":1636644.88,
    "OnePos":
    {
        "IDCoordinate":1,
        "AddressCounry":"Россия",
        "AddressCity":" Санкт-Петербург",
        "AddressRegiin":" Дворцовая площадь",
        "status":true,
        "x":30.3158684,
        "y":59.9390945
     },
     "TwoPos":
     {
        "IDCoordinate":2,
        "AddressCounry":"Россия",
        "AddressCity":" Свердловская область",
        "AddressRegiin":" болото Лих",
        "status":true,
        "x":59.9390945,
        "y":59.9390945
      }
  },
  {
    "distance":2124896.5,
    "OnePos":
    {
        "IDCoordinate":3,
        "AddressCounry":"Россия",
        "AddressCity":" Р-56",
        "AddressRegiin":" Р-56",
        "status":true,
        "x":31.2699146,
        "y":58.52281
     },
     "TwoPos":
     {
        "IDCoordinate":4,
        "AddressCounry":"Великобритания",
        "AddressCity":" Лондон",
        "AddressRegiin":" Whitehall (Уайтхолл)",
        "status":true,
        "x":-0.12766,
        "y":51.50735
      }
  }
]
