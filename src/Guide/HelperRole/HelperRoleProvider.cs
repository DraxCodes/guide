using Discord.WebSocket;
using Microsoft.Win32.SafeHandles;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Guide.HelperRole
{
    public class HelperRoleProvider : IHelperRoleProvider, IDisposable
    {
        public bool ShouldGetRole;

        private SocketGuildUser _user;
        private SocketRole _role;
        private bool _disposed = false;
        private SafeHandle _handle = new SafeFileHandle(IntPtr.Zero, true);
        public HelperRoleProvider(SocketGuildUser user, SocketVoiceState vsBefore, SocketVoiceState vsAfter)
        {
            if (ShouldGetHelper(vsBefore, vsAfter)) { ShouldGetRole = true; }
            else { ShouldGetRole = false; }
            _user = user;
            _role = user.Guild.Roles.FirstOrDefault(r => r.Id == Constants.HelperRoleId);
        }

        public void AddHelperRole()
        {
            _user.AddRoleAsync(_role);
        }

        public void RemoveHelperRole()
        {
            _user.RemoveRoleAsync(_role);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool ShouldGetHelper(SocketVoiceState vsBefore, SocketVoiceState vsAfter)
        {
            if (LeftAllVoiceChats(vsAfter) && LeftHelperChannel(vsBefore)) { return false; }
            else if (JoinedHelperChannel(vsAfter)) { return true; }
            return default;
        }

        private bool JoinedHelperChannel(SocketVoiceState vsAfter)
        {
            if (vsAfter.VoiceChannel?.Id == Constants.HelpVoiceChannelId) { return true; }
            return false;
        }

        private bool LeftHelperChannel(SocketVoiceState vsBefore)
        {
            if (vsBefore.VoiceChannel?.Id == Constants.HelpVoiceChannelId) { return true; }
            return false;
        }

        private bool LeftAllVoiceChats(SocketVoiceState vsAfter)
        {
            if (vsAfter.VoiceChannel is null || vsAfter.VoiceChannel?.Id != Constants.HelpVoiceChannelId) { return true; }
            return false;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) { return; }
            if (disposing) { _handle.Dispose(); }
            _disposed = true;
        }
    }
}
