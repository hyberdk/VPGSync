# Welcome to the VP->Google Sync (VPGSync) client.



* [Welcome to the VP->Google Sync (VPGSync) client.](#welcome-to-the-vp-google-sync-vpgsync-client)
 * [It does what?!?](#it-does-what)
 * [Why?](#Why?)
 * [Where to download?](#where-to-download)
 * [What works?](#what-works)
 * [What is synchronized?](#what-is-synchronized)
 * [Okay, so what does not work?](#okay-so-what-does-not-work)
* [How to use VPGSync](#how-to-use-vpgsync)
 * [Marking/Removing contacts for synchronization in VP](#markingremoving-contacts-for-synchronization-in-vp)
 * [Start Sync](#start-sync)
 * [Authorize Google Contact Access (first time only)](#authorize-google-contact-access-first-time-only)
 * [Syncing](#syncing)
* [Revoking access to Google](#revoking-access-to-google) 
* [Starting VPGSync automatically on logon](starting-vpgsync-automactically-on-logon)


**This project is only intended for people working at Vestas Wind Systems**

It is a synchronization tool for the internal VP (Vestas People) tool and to your personal Google Contacts. It is a one-way sync, hence only add/update/delete contacts in Google.

My internal initials are ESLAU if you need to get in contact with me.

**DISCLAMER - This is not the work of Vestas, this is just me as a private person, no one has approved or asked me to do it..**
**I’m doing it on my own free time, so if you use the software you are on your own, no creating tickets to IT Service, nor expecting me to help you out you are on your own**

## It does what?!?

It will download all contacts in the VP-DB that is marked for synchronization and add that to your Google Contacts.

All contacts in Google will have the label/group of "VPGSync". 

This will trigger an update..

* New VP Person is marked to be sync'ed -> Contact is created in Google
* VP Person is updated centrally (from SAP) -> Contact is updated in Google
* Google Contact is changed -> VP Person will overwrite your Google Contact
* VP Person leaves Vestas (changes status as former employee) -> Contact is deleted in Google Contact
* Sync mark removed on VP Person -> Contact is deleted in Google Contact.

This tool will only touch any Google Contact in the group/label "VPGSync"
This tool CANNOT update any VP Contact/Person..

## Why?

I really liked the old VPConnection, but the the Outlook App.. I dont care for it, like really not.. 

I can only mark one person at a time to me synced and it keeps fu..... up my contacts on my phone..

So I finally got so annoyed and decided to do something about it.

Yeah you might ask, why didnt you just build an Android app.. Well one reason: Mobile phones are restricted to the "mobility" network that does not have access to the VP-DB..

I did not want to make an application that broke our security policies.. So.. a Windows Native app it is...


## Where to download?

You can get it from here: https://github.com/hyberdk/VPGSync/releases/

Just grab the latest one ;-)


## What works?

* **v0.1**
 * VP People marked for "Outlook synchronization" will be synced
 * VP Departments marked for "Outlook synchronization" will be sync
  - Only the Department will be synced, not parent- or sub-departments will synchronize.
  - You can mark as many departments as you like.
* **v0.2**
 * It will sync the picture from photos.vestas.net to your Google Contact
 * AutoSync - Sync every xx minutes 
 * Minimize to tray


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

and you know, this was thrown together in 7-8 evenings, so not much error handling or logging is done.. So it will probably crash for you at some point.

# How to use VPGSync #

here is a quick guide to use it.

## Marking/Removing contacts for synchronization in VP ##

Go to http://vp

Find the contact/department you want to add/remove to your Google Contact and press the "Outlook synchronization"

#### Add Person ####

![vp_add_person_sync.png](images/vp_add_person_sync.png)

#### Remove Person ####
 
![vp_del_person_sync.png](images/vp_del_person_sync.png)

#### Add Department ####

![vp_add_department_sync.png](images/vp_add_department_sync.png)


#### Remove Department ####
 
![vp_del_department_sync.png](images/vp_del_department_sync.png)



## Start Sync ##

#### Start the program ####
Execute the VPGSync.exe file (you do not need to be administrator to run it)

The main window will appear

![Main Window](images/vpgsync_main_window.png)

**It is important that your initials are shown correct**

**AND you must be connected to the internal network, either in an office or via VPN**

When you are ready... Hold your breath and hit "Sync"

## Authorize Google Contact Access (first time only)

First time you synchronize on your PC it will ask you to confirm that the program can read/modify your contacts. Afterwards we will use a "Refresh Token" that does not require your involvement. You can always revoke the "Refresh Token" from your Google Account

A browser will pop up and ask you to log in:

![Google Login](images/google_login.png)

I use my personal account, you select the one you like to use.


![Google Login Allow Contact Access](images/google_login_allow_contacts_access.png)

Now allow VPGSync to read your contacts by hitting the "ALLOW" button

Once that is done, the focus will return to the VPGSync application..

## Syncing ##
While the application is running you can see the progress on how many contacts it found in VP, how many in Google, how many to create, update or delete.

You will see the numbers reflect the status of the sync.

Once its done it will look something like this:

![Main Window Done Sync](images/vpgsync_main_window_done.png)

You can now see your contacts in Google

Verify by visiting the https://contacts.google.com and check the "VPGSync" group

**THAT IT FOLKS!**

If you want to update, just hit the "Sync" button again.. ;-)


# Revoking access to Google # 

6 - steps, here we go:


![](images/remove_google_access_step1.png)

Login to Google and go to "My Account"



![](images/remove_google_access_step2.png)

Click "Apps with account access"


![](images/remove_google_access_step3.png)

Click "MANAGE APPS"


![](images/remove_google_access_step4.png)

Click the "VP->Google Sync (VPGSync)" app and then click "REMOVE ACCESS"


# Starting VPGSync automatically on logon

Open the Windows 8 start screen by doing one of the following:

* Press the Windows key Move mouse to the bottom left of the screen. When the start screen icon appears, click once. **OR** Press the ‘Windows key’+’R’

* Type %appdata% and press Enter. This should take you to “C:\Users\<User-Name>\AppData\Roaming”.

* Navigate to “\Microsoft\Windows\Start Menu\Programs\Startup”. The full path should now look something like: “C:\Users\<user-name>\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup”.

* Add shortcuts to the programs you want to automatically start into this folder.
