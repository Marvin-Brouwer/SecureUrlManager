# Threat protection

We'd like to provide a couple of mechanisms to automatically block urls.

Here are some ideas:

- <https://github.com/maravento/blackweb/tree/master/bwupdate/lst>
  We could just git-submodule this in, however it contains quite some links like youtube, so it'll need sanitization.
  Possibly we can just skip to line <https://github.com/maravento/blackweb/blob/master/bwupdate/lst/blockdomains.txt#L23>
  Maybe have some skip rules per file, if we know it's safe.
- <https://urlfiltering.paloaltonetworks.com/>
  Figure out if they have a free api
- <https://zeltser.com/malicious-ip-blocklists/>
  Most of these require some sort of registration/account.
  Perhaps we can pick a few and add setup instructions, just ignore them if they aren't setup in the appsettings?

And then maybe have a background task reading these 1x per day and distincting urls and ips in memory?