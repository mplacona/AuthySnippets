# AuthySnippets
A set of Authy snippets to be used with .NET

1. Run through the process described [here](https://www.microsoft.com/net/core#macos) to install .NET on your operating system.
2. Clone the repository
3. On your terminal navigate to the folder where you cloned the repository
4. Run `dotnet restore`
5. Uncomment the function you'd like to try on `Main`
6. Run `dotnet run`

## Push Notification Authentication with OneTouch
-------------

### Authy Create OneTouch Request

```bash
curl -X POST "http://api.authy.com/onetouch/$AUTHY_API_FORMAT/users/$AUTHY_ID/approval_requests” \
-H "X-Authy-API-Key: $AUTHY_API_KEY" \
-d message="$OT_MESSAGE" \
-d details="$OT_DETAILS" \
-d seconds_to_expire="$OT_TTL"
```

```csharp
CreateApprovalRequestAsync().Wait();
```

### Authy Check OneTouch Status
```bash
curl "http://api.authy.com/onetouch/$AUTHY_API_FORMAT/approval_requests/$UUID" \
-H "X-Authy-API-Key: $AUTHY_API_KEY"
```

```csharp
GetApprovalRequestAsync(UUID).Wait();
```

### Authy Request OneCode via SMS
```bash
curl "http://api.authy.com/protected/$AUTHY_API_FORMAT/sms/$AUTHY_ID?force=true" \
-H "X-Authy-API-Key: $AUTHY_API_KEY"
```

```csharp
RequestAuthySMSAsync().Wait()
```

### Authy Request OneCode via Voice
```bash
curl -i "http://api.authy.com/protected/$AUTHY_API_FORMAT/call/$AUTHY_ID?force=true" \
-H "X-Authy-API-Key: $AUTHY_API_KEY"
```

```csharp
RequestAuthyVoiceAsync().Wait();
```

### Authy Verify OneCode 
```bash
curl -i "http://api.authy.com/protected/$AUTHY_API_FORMAT/verify/$ONECODE/$AUTHY_ID" \
-H "X-Authy-API-Key: $AUTHY_API_KEY"
```

```csharp
VerifyTokenAsync().Wait();
```

## Smartphone generated TOTP with SoftToken
-------------

### Verify AUTHY SoftToken
```bash
curl -i "http://api.authy.com/protected/$AUTHY_API_FORMAT/verify/$ONECODE/$AUTHY_ID" \
-H "X-Authy-API-Key: $AUTHY_API_KEY"
```

```csharp
VerifyTokenAsync().Wait();
```

## Verify possession of phone
-------------

### Phone Verification Request
```bash
curl -X POST "https://api.authy.com/protected/$AUTHY_API_FORMAT/phones/verification/start?via=$VIA&country_code=$USER_COUNTRY&phone_number=$USER_PHONE" \ 
-H "X-Authy-API-Key: $AUTHY_API_KEY"
```

```csharp
StartPhoneVerificationAsync().Wait();
```

### Phone Verification Check
```bash
curl "https://api.authy.com/protected/$AUTHY_API_FORMAT/phones/verification/check?phone_number=$USER_PHONE&country_code=$USER_COUNTRY&verification_code=$VERIFY_CODE" \ 
-H "X-Authy-API-Key: $AUTHY_API_KEY"
```

```csharp
VerifyPhoneAsync().Wait();
```

