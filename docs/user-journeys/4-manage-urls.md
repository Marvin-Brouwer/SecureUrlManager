# Manage threat

As an admin, I would like to block a possibly malicious URL and mark it for review.

## User stories

> `TODO`

## API-flow

For the users:

> **Note:** both review and report do the same thing.
> However, they both have a different intent. The intent is shown in the review overview.
>
> You can mark a url for objective review if you visit it often.
> If you report a url as malicious it will get priority and possibly an email gets send to a security officer.

```mermaid
graph LR
    Report(( POST URL )) --> |"./{hash}/report-malicious" | Report_Inform
    Report(( POST URL )) --> |"./watchlist/review/hash/{hash}"| Report_ExistingUrl
    Report(( POST URL )) --> |"./watchlist/review/url/{hash}"| Report_ExistingUrl
    Report_ExistingUrl{Already registered?}
    Report_ExistingUrl --> |No| Report_MarkForReview[Mark the URL for review and return OK response]
    Report_ExistingUrl --> |Yes| Report_MarkForReview[Mark the URL for review and return OK response]

    Report_Inform["Mark for notification"] --> Report_ExistingUrl
```

For the admins:

> The overview page will return a grouped result with a group of non-reviewed items at the top.
>
> Additionally there may be a separate list of known malicious url we get from an api or a github repo.
> We may need a separate way to view these as read only?
> Or just group them at the bottom.

> **Note:** There is no delete functionality.
> If a url is no longer considered malicious you have to update the status instead of deleting it. The same goes for any of the other statuses.

```mermaid
graph LR
    List_WatchList --> |"./watchlist"| Report_ListWatch
    List_WatchList((GET URL)) --> |"./watchlist?page={page}"| Report_ListWatch
    Report_ListWatch["List all records of watchlist"]

    Get_WatchList((Get URL)) --> |"./watchlist/url/{url}"| Get_WatchListItem
    Get_WatchList --> |"./watchlist/hash/{hash}"| Get_WatchListItem
    Get_WatchListItem[Get the watchlist item data]


    Manage_WatchList((PUT URL)) --> |"./watchlist/{url}"| Manage_SetWatchLevel
    Manage_SetWatchLevel{Watch level}
    Manage_SetWatchLevel --> |Block| Manage_Ok[Set status in DB and return OK response]
    Manage_SetWatchLevel --> |Allow| Manage_Ok[Set status in DB and return OK response]
    Manage_SetWatchLevel --> |Unspecified| Manage_Ok[Set status in DB and return OK response]
```

> For security reasons we'd like to see how many users have visited the url in the last `month?` or so.
> On top of that we need information like who visited it by email address so we can handle a calamity.
> **Note:** The viewing of email addresses may be restricted to certain roles (like security admin) and may be hidden unless a url is reported to be blocked.

```mermaid
graph LR
    Visit(( GET URL )) --> |"./{hash}/metrics"| Visit_ExistingUrl

    Visit_ExistingUrl{Already registered?}
    Visit_ExistingUrl --> |No| Visit_ReturnNotFound[Return NotFound response]
    Visit_ExistingUrl --> |Yes| Visit_RegisterVisit[ Return MetaData response ]
```

## Wire-frames

> `TODO`
