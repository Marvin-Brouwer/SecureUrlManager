# Create named URL

As an admin, I'd like to create a named link for a URL.
  
EG:  
`https://www.mibo.com/room/lajsdfaoiue993lss` becomes
`https://urls.valtech.com/hr/newyears-mibo`

## User stories

> `TODO`

## API-flow

```mermaid
graph LR
    Start(( POST URL )) --> |"./{name}/{url}"| Validate

    Validate{Check URL validity}
    Validate --> |Not valid| ReturnError
    Validate --> |Unknown| MarkForReview([Mark the URL for review]) --> ExistingName
    Validate --> |Is valid| ExistingName
    
    ExistingName{Name Already registered}
    ExistingName --> |Yes| ExistingUrl
    ExistingName --> |No| GenerateNamedUrl
    
    ExistingUrl{Url Already registered}
    ExistingUrl --> |Yes| ReturnOk[Return ok response]
    ExistingUrl --> |No| GenerateNamedUrl
    
    GenerateNamedUrl[Generate Named Url]
```

## Wire-frames

> `TODO`
