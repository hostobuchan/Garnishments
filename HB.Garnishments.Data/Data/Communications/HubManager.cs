using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Communications
{
    public class HubManager
    {
        HubConnection WebHubConnection { get; set; }

        public delegate void HubAssetEventHandler(int accountId, string fileNo, byte debtor, int assetId, ulong assetInfoId);
        public delegate void HubGarnEventHandler(int accountId, string fileNo, byte debtor, int assetId, ulong assetInfoId, int requestId);
        public delegate void HubGarnProcessingEventHandler(int requestId, bool working, string userName);

        public event HubAssetEventHandler AssetUpdated;
        public event HubAssetEventHandler AssetAdded;
        public event HubGarnEventHandler GarnUpdated;
        public event HubGarnEventHandler GarnAdded;
        public event HubGarnProcessingEventHandler GarnWorking;

        public HubManager()
        {
            //this.WebHubConnection = new HubConnection("https://HBWEB.HOSTOBUCHAN.DOM/CLSWeb");
            //this.WebHubConnection = new HubConnection("http://localhost:63712/");
            this.WebHubConnection = new Microsoft.AspNetCore.SignalR.Client.HubConnectionBuilder()
                .WithUrl("https://HBWEB.HOSTOBUCHAN.DOM/CLSWeb/settlementHub", options =>
                {
                    options.UseDefaultCredentials = true;
                })
                .WithAutomaticReconnect()
                .AddNewtonsoftJsonProtocol()
                .Build();
            //this.WebHubConnection.Error += WebHubConnection_Error;
            this.WebHubConnection.On<int, string, byte, int, ulong>("AssetUpdate", (anid, fileno, debtor, aid, aiid) => { AssetUpdated?.Invoke(anid, fileno, debtor, aid, aiid); });
            this.WebHubConnection.On<int, string, byte, int, ulong, int>("AssetNew", (anid, fileno, debtor, aid, aiid, atid) => { AssetAdded?.Invoke(anid, fileno, debtor, aid, aiid); });
            this.WebHubConnection.On<int, string, byte, int, ulong, int>("GarnUpdate", (anid, fileno, debtor, aid, aiid, rid) => { GarnUpdated?.Invoke(anid, fileno, debtor, aid, aiid, rid); });
            this.WebHubConnection.On<int, string, byte, int, ulong, int>("GarnNew", (anid, fileno, debtor, aid, aiid, rid) => { GarnAdded?.Invoke(anid, fileno, debtor, aid, aiid, rid); });
            this.WebHubConnection.On<int, bool, string>("GarnWorking", (rid, working, username) => { GarnWorking?.Invoke(rid, working, username); });
        }

        public async Task Start()
        {
            if (this.WebHubConnection.State != HubConnectionState.Connected)
                await this.WebHubConnection.StartAsync();
        }
        public async Task Stop()
        {
            if (this.WebHubConnection.State == HubConnectionState.Connected)
                await this.WebHubConnection.StopAsync();
        }

        public async Task AssetUpdate(int accountId, string fileNo, byte debtor, int assetId, ulong assetInfoId)
        {
            await this.WebHubConnection?.InvokeAsync("SendAssetUpdate", accountId, fileNo, debtor, assetId, assetInfoId);
        }
        public async Task AssetUpdate(Data.Assets.AccountAssetInfo asset)
        {
            await AssetUpdate(asset.Account.ID, asset.Account.FileNo, asset.Debtor, asset.AssetName.ID, asset.Info.ID);
        }
        public async Task NewAsset(int accountId, string fileNo, byte debtor, int assetId, ulong assetInfoId, int assetType)
        {
            await this.WebHubConnection?.InvokeAsync("SendAssetNew", accountId, fileNo, debtor, assetId, assetInfoId, assetType);
        }
        public async Task NewAsset(Assets.AccountAssetInfo asset)
        {
            await NewAsset(asset.Account.ID, asset.Account.FileNo, asset.Debtor, asset.AssetName.ID, asset.Info.ID, (int)asset.Type);
        }
        public async Task GarnUpdate(int accountId, string fileNo, byte debtor, int assetId, ulong assetInfoId, int requestId)
        {
            await this.WebHubConnection?.InvokeAsync("GarnUpdate", accountId, fileNo, debtor, assetId, assetInfoId, requestId);
        }
        public async Task Garnupdate(Requests.AssetRequest request)
        {
            await GarnUpdate(request.Asset.Account.ID, request.Asset.Account.FileNo, request.Asset.Debtor, request.Asset.AssetName.ID, request.Asset.Info.ID, request.ID);
        }
        public async Task NewGarn(int accountId, string fileNo, byte debtor, int assetId, ulong assetInfoId, int requestId)
        {
            await this.WebHubConnection?.InvokeAsync("NewGarn", accountId, fileNo, debtor, assetId, assetInfoId, requestId);
        }
        public async Task NewGarn(Requests.AssetRequest request)
        {
            await NewGarn(request.Asset.Account.ID, request.Asset.Account.FileNo, request.Asset.Debtor, request.Asset.AssetName.ID, request.Asset.Info.ID, request.ID);
        }
        public async Task WorkingGarn(int requestId, bool working, string userName)
        {
            await this.WebHubConnection?.InvokeAsync("SendGarnWorking", requestId, working, userName);
        }
        public async Task WorkingGarn(Requests.AssetRequest request, bool working, string userName)
        {
            await WorkingGarn(request.ID, working, userName);
        }



        private void WebHubConnection_Error(Exception ex)
        {
            // How do we handle failures? Do we care?
        }
    }
}
