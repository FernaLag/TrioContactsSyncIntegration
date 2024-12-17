namespace Trio.ContactSync.Domain
{
    public record ContactSyncResult(int SyncedContacts, List<Contact> Contacts);
}