---
title: Testing your API with HttpRepl
description: Testing your API with HttpRepl
author: cwoodruff
---
# Testing your API with HttpRepl

Test your APIs with the HttpRepl

## INSTALL HTTPREPL TOOLING FOR DOTNET

```dos
dotnet tool install -g Microsoft.dotnet-httprepl
```

## TEST TO CONFIRM INSTALLED

```dos
httprepl --help
```

## HOW TO TEST YOUR WEB API

```dos
httprepl <ROOT URI>
```

### EXAMPLE

```dos
httprepl https://localhost:5001
```

### ALTERNATIVE

```dos
connect <ROOT URI>
```

## USE HTTPREPL WITH OPENAPI

```dos
connect <ROOT URI> --openapi <OPENAPI DESCRIPTION ADDRESS>
```

### EXAMPLE

```dos
connect https://localhost:5001 --openapi /swagger/v1/swagger.json
```

## TO GET MORE DETAILS WITH HTTPREPL USE VERBOSE

```dos
connect <ROOT URI> --verbose
```

### EXAMPLE

```dos
connect https://localhost:5001 --verbose
Checking https://localhost:5001/swagger.json... 404 NotFound
Checking https://localhost:5001/swagger/v1/swagger.json... 404 NotFound
Checking https://localhost:5001/openapi.json... Found
Parsing... Successful (with warnings)
The field 'info' in 'document' object is REQUIRED [#/info]
The field 'paths' in 'document' object is REQUIRED [#/paths]
```

## NAVIGATE THE WEB API

### VIEW AVAILABLE ENDPOINTS (AFTER CONNECTED)

```dos
https://localhost:5001/> ls
```

### NAVIGATE TO AN SPECIFIC ENDPOINT

```dos
https://localhost:5001/> cd people
```

### RESPONSE

```dos
/people    [get|post]

https://localhost:5001/people>
```

## TEST AN API ENDPOINT (GET ALL)

```dos
https://localhost:5001/people> get

## TEST AN API ENDPOINT (GET BY ID)

```dos
https://localhost:5001/people> get 2
```

## TEST AN API ENDPOINT (POST)

```dos
https://localhost:5001/people> post -h Content-Type=application/json
```

The default text editor opens a .tmp file with a JSON template representing the HTTP request body.

## TEST AN API ENDPOINT (PUT)

```dos
https://localhost:5001/fruits> put 2 -h Content-Type=application/json
```

Modify the JSON template to satisfy model validation requirements

## TEST AN API ENDPOINT (DELETE)

```dos
https://localhost:5001/fruits> delete 2
```
