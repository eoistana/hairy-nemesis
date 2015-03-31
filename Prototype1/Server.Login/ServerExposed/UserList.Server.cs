using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Login
{
  public partial class UserList
  {
    partial void OnProcessAddUserMessage(AddUserMessage message)
    {
      var existing = SelectUsers().FirstOrDefault(u => u.Name == message.Name);
      if (existing == null)
      {
        AddUsers(new User(message.Name, message.Password));
      }
    }

    partial void OnProcessLogoutUserMessage(LogoutUserMessage message)
    {
      var existing = SelectTokens().FirstOrDefault(t => t.Token == message.Token);
      if (existing != null) RemoveTokens(existing);
    }

    partial void OnProcessVerifyTokenMessage(VerifyTokenMessage message, UserList.VerifyTokenMessageReturnValue returnValue)
    {
      var existing = SelectTokens().FirstOrDefault(t => t.Token == message.Token);
      var valid = false;
      if (existing != null) 
      {
        valid = true;
        existing.LastAccessed = DateTime.Now;
      }
      returnValue.ReturnValue = new VerifyTokenResponseMessage(valid);
    }
  }
}
