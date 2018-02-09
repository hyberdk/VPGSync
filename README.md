# Welcome to the VP->Google Sync (VPGSync) client.

This project is only intended for people working at Vestas Wind Systems

It is a synchronization tool for the internal VP (Vestas People) tool and to your personal Google Contacts. It is a one-way sync, hence only add/update/delete contacts in Google.

My internal initials are ESLAU if you need to get in contact with me.

**DISCLAMER - This is not the work of Vestas, this is just me as a private person, no one has approved or asked me to do it..**
**I’m doing it on my own free time, so if you use the software you are on your own, no creating tickets to IT Service, nor expecting me to help you out you are on your own**

## Why?

The Outlook App.. I hate it, like really really hate it.. But I am forced to use it, like really really forced to use it.

I cannot believe that M$ get away with pushing such s... to people, damn..

I can only mark one person at a time to me synced and it keeps fu..... up my contacts on my phone..

So I finally got so annoyed and decided to do something about it.


## How it works?

It will download all contacts in the VP-DB that is marked for synchronization and add that to your Google Contacts.

**Ohh, FYI.. You have to be connected to the internal network, either directly or on VPN as the VP-DB is internally only**

All contacts in Google will have the label/group of "VPGSync". This will trigger a sync..

* New VP Person is marked to be sync'ed -> Contact is created in Google
* VP Person is updated centrally (from SAP) -> Contact is updated in Google
* Google Contact is changed -> VP Person will overwrite your Google Contact
* VP Person leaves Vestas (changes status as former employee) -> Contact is deleted in Google Contact
* Sync mark removed on VP Person -> Contact is deleted in Google Contact.

This tool will only touch any Google Contact in the group/label "VPGSync"
This tool CANNOT update any VP Contact/Person..

## What works?

* VP People marked for "Outlook synchronization" will be synced
* VP Departments marked for "Outlook synchronization" will be sync
 - Only the Department will be synced, not parent- or sub-departments will synchronize.
 - You can mark as many departments as you like.

## What is synchronized?

From VP field | | To Google field
--------- | --- | ---------
Signature | --> | Nickname
Telephone | --> | Work Phone
Mobile telephone | --> | Work Mobile
Private mobile telephone | --> | Private Mobile
Department | --> | Company
Position | --> | Job Title
E-mail | --> | Work Email
Private E-Mail | --> | Private Email

## Okay, so what does not work?

Probably a lot.. like a lot..

But known outstanding is:

* Sites marked to be synchronized
* Picture is not uploaded to Google Contacts

and you know, this was thrown together in 7-8 evenings, so not much error handling or logging is done.. So it will probably crash for you at some point.
