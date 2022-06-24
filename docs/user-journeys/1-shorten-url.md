# Shorten URL

As a user, I'd like to apply a URL to shorten into a neat link.
  
EG:  
`https://docs.github.com/en/actions/learn-github-actions/contexts#github-context` becomes
`https://urls.valtech.com/3nlard098uf`

## User stories

> `TODO`

## API-flow

```mermaid
graph LR
    Start(( POST URL )) --> |"./shorten"| Validate

    Validate{Check URL validity}
    Validate --> |Not valid| ReturnError
    Validate --> |Unknown| ExistingUrl
    Validate --> |Is valid| ExistingUrl
    
    ExistingUrl{Already registered}
    ExistingUrl --> |Yes| ReturnOk[Return ok response]
    ExistingUrl --> |No| GenerateShortUrl[Generate Short Url]
```

## Wire-frames

> `TODO`
