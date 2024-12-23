# **Trio.ContactSync**

A robust API that integrates with **MockApi** and **Mailchimp** to synchronize contacts.

[Access the application here](http://triocontactsync.runasp.net/index.html)

---

### References

- [**Technical Design**](https://www.notion.so/Trio-ContactSync-Technical-Design-15b322dfed9a80ecbd96cd8d827c96c5?pvs=21)
- [**Video Walkthrough**](https://youtu.be/vHuIoCD_dJg)

---

## **Setup**

1) Install **Visual Studio**.
2) [Clone](https://docs.github.com/en/repositories/creating-and-managing-repositories/cloning-a-repository) the repository.
3) Open `appsettings.json`, located inside the **Trio.ContactsSync.Api** project.
---
   ![image](https://github.com/user-attachments/assets/8ee6690a-74e1-466e-9ad8-1c4868fa9df4)


### **Configure the `appsettings.json` with your Mailchimp information.**

```json
{
  "**Mailchimp**": {
    "ApiKey": "YOUR_API_KEY",
    "ListId": "YOUR_LIST_ID",
    "BaseAddress": "https://**<dc>**.api.mailchimp.com/3.0/"
  }
}
```

### Create an Mailchimp account

If you don't have a **Mailchimp** account already, you’ll need to [create one](https://login.mailchimp.com/signup/).

### **Generate your `API key`**

Navigate to the [API Keys section](https://us1.admin.mailchimp.com/account/api/) of your Mailchimp account.

Click **Create New Key** and copy the key **immediately**.

You won’t be able to see or copy the key again.

### Get your `ListId`

Navigate to the [Audience Settings](https://admin.mailchimp.com/audience/settings/).

Scroll down and retrieve the **Unique Audience ID,** which is your **ListId.**

### Find your `BaseAddress`

- Navigate to [admin.mailchimp.com](http://admin.mailchimp.com/)

Look at the URL in your browser, you’ll see something like `https://**us19**.admin.mailchimp.com/` 

The **`us19`** part is your **server prefix**. Note that your specific value may vary.

- Replace **`<dc>`** in `https://<dc>.api.mailchimp.com/3.0/` with your **server prefix.**

**BaseAddress example** 

**`https://us19.api.mailchimp.com/3.0/`** 

---

### **Run the project**

```bash
dotnet run --project Trio.ContactSync.Api
```

---

### **Access Swagger**

- **`https://localhost:7090/`**

---

## **Testing**

1. Navigate to the **Test Explorer** in Visual Studio.
2. Run all tests.

**Test Highlights**

- **Integration Tests**: Verify communication with Mock API and Mailchimp's API.
- **Unit Tests**: Validate Contact Sync business logic.

---

## **Future Improvements**

- Create validations for clients, e.g., a maximum number of operations per batch
- CI/CD pipeline for automated deployment
- Enhance swagger documentation
- Endpoint to get all Mailchimp members
- Implement a logging component
- Enhance test coverage

---

## **About the Author**

Developed by [Ferna](https://github.com/FernaLag) - a passionate software engineer specializing in .NET solutions, cloud integration, and clean architecture.
