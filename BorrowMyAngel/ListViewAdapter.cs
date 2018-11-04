using Android.Content;
using Android.Views;
using Android.Widget;
using Java.Lang;
using System.Collections.Generic;

namespace BorrowMyAngel
{
	public class ListViewAdapter : BaseAdapter
    {
        private ChatRoomActivity mainActivity;  
        private List<MessageData> lstMessage;  
   
        public ListViewAdapter(ChatRoomActivity mainActivity, List<MessageData> lstMessage)  
        {  
            this.mainActivity = mainActivity;  
            this.lstMessage = lstMessage;  
        }  
   
        public override int Count  
        {  
            get { return lstMessage.Count; }  
        }  
   
        public override Object GetItem(int position)  
        {  
            return position;   
        }  
   
        public override long GetItemId(int position)  
        {  
            return position;  
        }  
   
        public override View GetView(int position, View convertView, ViewGroup parent)  
        {  
            LayoutInflater inflater = (LayoutInflater)mainActivity.BaseContext.GetSystemService(Context.LayoutInflaterService);  
            View ItemView = inflater.Inflate(Resource.Layout.ChatMessageTemplate, null);  
            TextView message_user, message_time, message_content;  
            message_user = ItemView.FindViewById<TextView>(Resource.Id.message_user);  
            message_time = ItemView.FindViewById<TextView>(Resource.Id.message_time);  
            message_content = ItemView.FindViewById<TextView>(Resource.Id.message_text);  
   
            message_user.Text = lstMessage[position].uID;  
            message_time.Text = lstMessage[position].timestamp;  
            message_content.Text = lstMessage[position].data;  
   
            return ItemView;  
        }
    }
}
