/***************************************************************************
 * 
 *       功能：     系统消息持久层基类
 *       作者：     彭伟
 *       日期：     2007-03-22
 * 
 * *************************************************************************/
namespace SmartSoft.Persistence.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using IBatisNet.Common;
    using IBatisNet.DataMapper;
    using IBatisNet.Common.Exceptions;

    using SmartSoft.Domain.Common;
    using SmartSoft.Domain.Purview;

    public class MessageSqlMapDao : BaseSqlMapDao
    {
        public MessageSqlMapDao()
        {

        }

        #region 系统消息
        /// <summary>
        /// 得到要提醒的消息
        /// </summary>
        /// <param name="opeID"></param>
        /// <returns></returns>
        public IList<sysMessage> SelectShowMessageList(int opeID)
        {
            return ExecuteQueryForList<sysMessage>("SelectShowMessageList", opeID);
        }

        /// <summary>
        /// 得到我收到的消息
        /// </summary>
        /// <param name="opeID"></param>
        /// <returns></returns>
        public IList<sysMessage> SelectReceiveMessageList(int opeID)
        {
            IList<sysMessage> messageList = ExecuteQueryForList<sysMessage>("SelectReceiveMessageList", opeID);


            return messageList;
        }

        /// <summary>
        /// 得到我发送的消息
        /// </summary>
        /// <param name="opeID"></param>
        /// <returns></returns>
        public IList<sysMessage> SelectSendMessageList(int opeID)
        {
            return ExecuteQueryForList<sysMessage>("SelectSendMessageList", opeID);
        }

        /// <summary>
        /// 新增消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public int InsertMessage(sysMessage message)
        {
            int id = GetId("sysMessage");
            message.MessageID = id;
            ExecuteInsert("InsertSysMessageUSP", message);
            return id;
        }

        /// <summary>
        /// 新增消息查看人
        /// </summary>
        /// <param name="messageid"></param>
        /// <param name="operatorid"></param>
        public void InsertMessageLooker(int messageid, int operatorid)
        {
            sysMessage_Looker looker = new sysMessage_Looker();
            looker.MessageID = messageid;
            looker.ObjectID = operatorid;
            ExecuteInsert("InsertsysMessage_Looker", looker);
        }

        public bool SelectMessageIsRead(int messageid, int operatorid)
        { 
            Hashtable ht = new Hashtable();
            ht.Add("MessageID",messageid);
            ht.Add("OperatorID",operatorid);
            if (ExecuteQueryForList<sysMessage_Readed>("SelectMessageIsReaded", ht).Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 设置消息已读
        /// </summary>
        /// <param name="messageid"></param>
        /// <param name="operatorid"></param>
        public void InsertMessageReaded(int messageid, int operatorid)
        {
            Hashtable ht = new Hashtable();
            ht.Add("MessageID",messageid);
            ht.Add("OperatorID",operatorid);
            if (ExecuteQueryForList<sysMessage_Readed>("SelectMessageIsReaded", ht).Count > 0)
            {
            }
            else
            {
                sysMessage_Readed read = new sysMessage_Readed();
                read.MessageID = messageid;
                read.OperatorID = operatorid;
                ExecuteInsert("InsertsysMessage_Readed", read);
            }
        }

        /// <summary>
        /// 设置消息已被删除
        /// </summary>
        /// <param name="messageid"></param>
        /// <param name="operatorid"></param>
        public void InsertMessageDeleted(int messageid, int operatorid)
        {
            sysMessage_Deleted del = new sysMessage_Deleted();
            del.MessageID = messageid;
            del.OperatorID = operatorid;
            ExecuteInsert("InsertsysMessage_Deleted", del);
        }

        /// <summary>
        /// 设置消息未被删除
        /// </summary>
        /// <param name="messageid"></param>
        /// <param name="operatorid"></param>
        public void DeleteMessage_Deleted(int messageid, int operatorid)
        {
            Hashtable ht = new Hashtable();
            ht.Add("MessageID", messageid);
            ht.Add("OperatorID", operatorid);

            ExecuteDelete("DeletesysMessage_Deleted", ht);
        }

        
        public void DeleteMessage_Looker(int messageid, int operatorid)
        {
            Hashtable ht = new Hashtable();
            ht.Add("MessageID", messageid);
            ht.Add("OperatorID", operatorid);

            ExecuteDelete("DeletesysMessage_Looker", ht);
        }

        /// <summary>
        /// 设置消息未读
        /// </summary>
        /// <param name="messageid"></param>
        /// <param name="operatorid"></param>
        public void DeleteMessage_Readed(int messageid, int operatorid)
        {
            Hashtable ht = new Hashtable();
            ht.Add("MessageID", messageid);
            ht.Add("OperatorID", operatorid);

            ExecuteDelete("DeletesysMessage_Readed", ht);
        }

        /// <summary>
        /// 得到消息明细
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public sysMessage GetMessageDetail(int messageId)
        {
            return ExecuteQueryForObject<sysMessage>("GetMessageDetail", messageId);
        }

        /// <summary>
        /// 删除消息
        /// </summary>
        /// <param name="messageId"></param>
        public void DeleteMessage(int messageId)
        {
            ExecuteDelete("DeleteMessage", messageId);
        }

        /// <summary>
        /// 修改消息
        /// </summary>
        /// <param name="messageId"></param>
        public void UpdateMessage(sysMessage message)
        {
            ExecuteUpdate("UpdateSysMessageUSP", message);
        }

        /// <summary>
        /// 修改可以浏览的人
        /// </summary>
        /// <param name="operatorID"></param>
        /// <param name="messageID"></param>
        public void UpdatesysMessage_LookerByMessageID(int operatorID, int messageID)
        {
            Hashtable ht = new Hashtable();
            ht["ObjectID"] = operatorID;
            ht["MessageID"] = messageID;
            ExecuteUpdate("UpdatesysMessage_LookerByMessageID", messageID);
        }

        #endregion

        #region 首页

        public IList<sysMessage> SelectReceiveMessageListByCount(int count, int opeId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("COUNT", count);
            ht.Add("opeID", opeId);

            return ExecuteQueryForList<sysMessage>("SelectReceiveMessageListByCount", ht);
        }

        #endregion
    }
}
