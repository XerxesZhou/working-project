/***************************************************************************
 * 
 *       ���ܣ�     ϵͳ��Ϣ�����ӿ�
 *       ���ߣ�     ��ΰ
 *       ���ڣ�     2007-03-22
 * 
 *       �޸����ڣ� 
 *       �޸��ˣ�
 *       �޸����ݣ�
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
        #region ϵͳ��Ϣ
        /// <summary>
        /// �õ�Ҫ���ѵ���Ϣ
        /// </summary>
        /// <param name="opeID"></param>
        /// <returns></returns>
        IList<sysMessage> SelectShowMessageList(int opeID);

        /// <summary>
        /// �õ����յ�����Ϣ
        /// </summary>
        /// <param name="opeID"></param>
        /// <returns></returns>
        IList<sysMessage> SelectReceiveMessageList(int opeID);

        /// <summary>
        /// �õ��ҷ��͵���Ϣ
        /// </summary>
        /// <param name="opeID"></param>
        /// <returns></returns>
        IList<sysMessage> SelectSendMessageList(int opeID);

        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        int InsertMessage(sysMessage message, ArrayList al);

        /// <summary>
        /// �õ���Ϣ��ϸ
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        sysMessage GetMessageDetail(int messageId);

        /// <summary>
        /// ������Ϣ�Ѷ�
        /// </summary>
        /// <param name="messageid"></param>
        /// <param name="operatorid"></param>
        void InsertMessageReaded(int messageid, int operatorid);


        /// <summary>
        /// ������Ϣ�ѱ�ɾ��
        /// </summary>
        /// <param name="messageid"></param>
        /// <param name="operatorid"></param>
        void InsertMessageDeleted(int messageid, int operatorid);


        /// <summary>
        /// ������Ϣδ��
        /// </summary>
        /// <param name="messageid"></param>
        /// <param name="operatorid"></param>
        void DeleteMessage_Readed(int messageid, int operatorid);

        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        /// <param name="messageId"></param>
        void DeleteMessage(int messageId);

        /// <summary>
        /// �޸���Ϣ
        /// </summary>
        /// <param name="messageId"></param>
        void UpdateMessage(sysMessage message);

        /// <summary>
        /// �޸Ŀ����������
        /// </summary>
        /// <param name="operatorID"></param>
        /// <param name="messageID"></param>
        void UpdatesysMessage_LookerByMessageID(int operatorID, int messageID);

        /// <summary>
        /// �õ��Ƿ��Ѷ�
        /// </summary>
        /// <param name="operatorID"></param>
        /// <param name="messageID"></param>
        bool SelectMessageIsRead(int messageid, int operatorid);

        #endregion

        #region ��ҳ

        IList<sysMessage> SelectReceiveMessageListByCount(int count, int opeId);

        #endregion
    }
}
