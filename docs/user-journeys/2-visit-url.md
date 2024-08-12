# Visit URL

As a user, I'd like to visit a shortened URL and get to see my content.
  
EG:  

`https://urls.{company}.com/3nlard098uf` will redirect to
`https://docs.github.com/en/actions/learn-github-actions/contexts#github-context`

When a user get's the eventual preview page when a visited url is neither Blocked or Valid a user should have the chance to report a URL to be blocked.

## User stories

> `TODO`

## API-flow

```mermaid
graph LR
    Visit(( GET URL )) --> |"./{hash}"| Visit_ExistingUrl

    Visit_ExistingUrl{In database?}
    Visit_ExistingUrl --> |No| Visit_ReturnNotFound[Return NotFound response]
    Visit_ExistingUrl --> |Yes| Visit_Validate

    Visit_Validate{Check URL validity}
    Visit_Validate --> |Is valid| Visit_ReturnRedirectResponse[Return Redirect response to long URL]
    Visit_Validate --> |Unknown| Visit_RegisterUnknownVisit([ Register unknown visit count in AI ]) --> ReturnPreviewResponse["`Return Response with long URL and OG data / sandboxed iframe perhaps?`"]
    Visit_Validate --> |Not valid| Visit_ReturnError[Return Error response with additional data]
```

> **Note:**
> Because we don't want to have to allowlist every unknown URL that is shortened we measure the amount of visits and use the counter to determine whether it needs to be added to the valid URL list.

> **Note:**
> When a URL is blocked by an admin either for being malicious or if it's on the block list a user will see an error page, however the API response shall be similar to when an unknown url is loaded.

## Wire-frames

> `TODO`
