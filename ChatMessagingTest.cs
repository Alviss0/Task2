using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using System;

namespace Lab3Messages_Test
{
    public class ChatMessagingTest
    {
        [Test]
        public void Test1()
        {
            Chat chat = new Chat("Chat_1");
            
            MockUser firstUser = new MockUser("Lucas");
            firstUser.JoinToChat(chat, 1);
            
            MockUser secondUser = new MockUser("Roma");
            secondUser.JoinToChat(chat, 2);
            
            MockUser thirdUser = new MockUser("Anna");
            thirdUser.JoinToChat(chat, 3);
            
            firstUser.SandMess("Hello everyone!", chat);
            
            Thread.Sleep(4000);
            
            Assert.AreEqual(1, secondUser.ReceivedMess.Count);
            Assert.AreEqual("Hello everyone!", secondUser.ReceivedMess[0].Content);
            Assert.AreEqual(firstUser, secondUser.ReceivedMess[0].Sender.User);
        }

        private class MockUser : User
        {
            public List<ChatMess> ReceivedMess { get; }

            public MockUser(string name) : base(name)
            {
                ReceivedMess = new List<ChatMess>();
            }

            public override void ReceiveMess(ChatMess message)
            {
                ReceivedMess.Add(message);
            }
        }
    }

    #region classes
    public class Chat
    {
        public string Name { get; }
        private List<ChatMembership> _members;
        private List<ChatMess> _messages;

        public Chat(string name)
        {
            Name = name;
            _members = new List<ChatMembership>();
            _messages = new List<ChatMess>();
        }

        public void AddMember(ChatMembership membership)
        {
            _members.Add(membership);
        }

        public void PublishMess(ChatMess message)
        {
            _messages.Add(message);
            foreach (var member in _members)
            {
                new Thread(() => member.Notify(message)).Start();
            }
        }
    }

    public class ChatMembership
    {
        public Chat Chat { get; }

        public User User { get; }

        public int ReceivedMessageDelay { get; }

        public ChatMembership(Chat chat, User user, int receivedMessageDelay)
        {
            Chat = chat;
            User = user;
            ReceivedMessageDelay = receivedMessageDelay;
        }

        public void Notify(ChatMess message)
        {
            Thread.Sleep(ReceivedMessageDelay * 1000);
            User.ReceiveMess(message);
        }
    }

    public class ChatMess
    {
        public string Content { get; }

        public ChatMembership Sender { get; }

        public ChatMess(string content, ChatMembership sender)
        {
            Content = content;
            Sender = sender;
        }
    }

    public class User
    {
        public string Name { get; }

        private List<ChatMembership> _subscribtions;

        public User(string name)
        {
            Name = name;
            _subscribtions = new List<ChatMembership>();
        }

        public void JoinToChat(Chat chat, int delay)
        {
            ChatMembership membership = new ChatMembership(chat, this, delay);
            chat.AddMember(membership);
            _subscribtions.Add(membership);
        }

        public void SandMess(string content, Chat chat)
        {
            ChatMembership chatMembership = _subscribtions.Find(subscription => subscription.Chat.Equals(chat));
            ChatMess message = new ChatMess(content, chatMembership);
            chat.PublishMess(message);
        }

        public virtual void ReceiveMess(ChatMess mess)
        {
            string dateTimeString = DateTime.Now.ToString();
            Console.WriteLine(dateTimeString + " User " + Name +
                              " received message from user " + mess.Sender.User.Name +
                              " in chat " + mess.Sender.Chat.Name +
                              ". Message content: " + mess.Content);
        }
    }
    #endregion
}