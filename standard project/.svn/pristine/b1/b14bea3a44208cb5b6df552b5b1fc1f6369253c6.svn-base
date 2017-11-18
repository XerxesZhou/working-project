/***************************************************************************
 * 
 *       功能：     系统消息服务层接口
 *       作者：     彭伟
 *       日期：     2007-03-22
 * 
 *       修改日期： 
 *       修改人：
 *       修改内容：
 * 
 * *************************************************************************/
namespace SmartSoft.Service.Interface.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using SmartSoft.Domain.Common;

    public interface IMessageService
    {
        #region 系统消息
        /// <summary>
        /// 得到要提醒的消息
        /// </summary>
        /// <param name="opeID"></param>
        /// <returns></returns>
        IList<sysMessage> SelectShowMessageList(int opeID);

        /// <summary>
        /// 得到我收到的消息
        /// </summary>
        /// <param name="opeID"></param>
        /// <returns></returns>
        IList<sysMessage> SelectReceiveMessageList(int opeID);

        /// <summary>
        /// 得到我发送的消息
        /// </summary>
        /// <param name="opeID"></param>
        /// <returns></returns>
        IList<sysMessage> SelectSendMessageList(int opeID);

        /// <summary>
        /// 新增消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        int InsertMessage(sysMessage message, ArrayList al);

        /// <summary>
        /// 得到消息明细
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        sysMessage GetMessageDetail(int messageId);

        /// <summary>
        /// 设置消息已读
        /// </summary>
        /// <param name="messageid"></param>
        /// <param name="operatorid"></param>
        void InsertMessageReaded(int messageid, int operatorid);


        /// <summary>
        /// 设置消息已被删除
        /// </summary>
        /// <param name="messageid"></param>
        /// <param name="operatorid"></param>
        void InsertMessageDeleted(int messageid, int operatorid);


        /// <summary>
        /// 设置消息未读
        /// </summary>
        /// <param name="messageid"></param>
        /// <param name="operatorid"></param>
        void DeleteMessage_Readed(int messageid, int operatorid);

        /// <summary>
        /// 删除消息
        /// </summary>
        /// <param name="messageId"></param>
        void DeleteMessage(int messageId);

        /// <summary>
        /// 修改消息
        /// </summary>
        /// <param name="messageId"></param>
        void UpdateMessage(sysMessage message);

        /// <summary>
        /// 修改可以浏览的人
        /// </summary>
        /// <param name="operatorID"></param>
        /// <param name="messageID"></param>
        void UpdatesysMessage_LookerByMessageID(int operatorID, int messageID);

        /// <summary>
        /// 得到是否已读
        /// </summary>
        /// <param name="operatorID"></param>
        /// <param name="messageID"></param>
        bool SelectMessageIsRead(int messageid, int operatorid);

        #endregion

        #region 首页

        IList<sysMessage> SelectReceiveMessageListByCount(int count, int opeId);

        #endregion
    }
}
