# Visit URL

As a user, I'd like to visit a shortened URL and get to see my content.
  
EG:  

`https://urls.valtech.com/3nlard098uf` will redirect to
`https://docs.github.com/en/actions/learn-github-actions/contexts#github-context`

When a user get's the eventual preview page when a visited url is neither Blocked or Valid a user should have the chance to report a URL to be blocked.

## User stories

> `TODO`

## API-flow

```mermaid
graph LR
    Visit(( GET URL )) --> |"./{hash}"| Visit_ExistingUrl

    Visit_ExistingUrl{Already registered?}
    Visit_ExistingUrl --> |No| Visit_ReturnNotFound[Return NotFound response]
    Visit_ExistingUrl --> |Yes| Visit_RegisterVisit([ Register visit ]) --> Visit_Validate

    Visit_Validate{Check URL validity}
    Visit_Validate --> |Is valid| Visit_ReturnRedirectResponse[Return Redirect response to long URL]
    Visit_Validate --> |Unknown| Visit_RegisterUnknownVisit([ Register unknown visit count in AI ]) --> ReturnPreviewResponse[Return Response with long URL and OG data]
    Visit_Validate --> |Not valid| Visit_ReturnError[Return Error response with additional data]

    Report(( PUT URL )) --> |"./{hash}/report"| Report_ExistingUrl
    Report_ExistingUrl{Already registered?}
    Report_ExistingUrl --> |No| Report_ReturnNotFound[Return NotFound response]
    Report_ExistingUrl --> |Yes| Report_MarkForReview[Mark the URL for review and return OK response]
```

> **Note:**
> Because we don't want to have to whitelist every unknown URL that is shortened we measure the amount of visits and use the counter to determine whether it needs to be added to the valid URL list.

> **Note:**
> When a URL is blocked by an admin either for being malicious or if it's on the block list a user will see a different page, however the API response shall be similar.

## Wire-frames

> `TODO`
