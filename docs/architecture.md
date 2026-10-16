# Architecture

```text
Client
  |
  v
ASP.NET Core controller -> IUsersService -> configured HttpClient -> sample external API
                                      |
                                      +--> mocked HTTP in unit tests

Dockerfile / Compose package the API for local execution.
```

The API wraps a public sample-data source through a typed service and configuration object. Unit tests use mocked HTTP behavior to exercise controller/service paths without depending on the external service. Docker assets provide a local container exercise.

## Boundary

The external endpoint is demonstration data, not an owned integration contract. The repository has no persistent data model or production service commitments.
