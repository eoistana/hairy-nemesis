using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Login
{
  public partial class UserList
  {
    partial void OnUserListInit()
    {
      Users = new List<User>();
      Tokens = new List<TokenUsage>();
    }

    partial void OnProcessLoginUserMessage(LoginUserMessage message, UserList.LoginUserMessageReturnValue returnValue)
    {
      var user = new User(message.Name, message.Password);
      var existing = SelectUsers().FirstOrDefault(u => u.Name == user.Name && u.Password == user.Password);
      if (existing == null) returnValue.ReturnValue = new LoginUserResponseMessage();
      else
      {
        var token = new LoginToken(Guid.NewGuid().ToString());
        AddTokens(new TokenUsage(token) { User = user, LastAccessed = DateTime.Now });
        returnValue.ReturnValue = new LoginUserResponseMessage(token);
      }
    }
  }
}
