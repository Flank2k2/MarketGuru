(function(t){function e(e){for(var r,i,s=e[0],c=e[1],l=e[2],d=0,v=[];d<s.length;d++)i=s[d],Object.prototype.hasOwnProperty.call(n,i)&&n[i]&&v.push(n[i][0]),n[i]=0;for(r in c)Object.prototype.hasOwnProperty.call(c,r)&&(t[r]=c[r]);u&&u(e);while(v.length)v.shift()();return o.push.apply(o,l||[]),a()}function a(){for(var t,e=0;e<o.length;e++){for(var a=o[e],r=!0,s=1;s<a.length;s++){var c=a[s];0!==n[c]&&(r=!1)}r&&(o.splice(e--,1),t=i(i.s=a[0]))}return t}var r={},n={app:0},o=[];function i(e){if(r[e])return r[e].exports;var a=r[e]={i:e,l:!1,exports:{}};return t[e].call(a.exports,a,a.exports,i),a.l=!0,a.exports}i.m=t,i.c=r,i.d=function(t,e,a){i.o(t,e)||Object.defineProperty(t,e,{enumerable:!0,get:a})},i.r=function(t){"undefined"!==typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(t,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(t,"__esModule",{value:!0})},i.t=function(t,e){if(1&e&&(t=i(t)),8&e)return t;if(4&e&&"object"===typeof t&&t&&t.__esModule)return t;var a=Object.create(null);if(i.r(a),Object.defineProperty(a,"default",{enumerable:!0,value:t}),2&e&&"string"!=typeof t)for(var r in t)i.d(a,r,function(e){return t[e]}.bind(null,r));return a},i.n=function(t){var e=t&&t.__esModule?function(){return t["default"]}:function(){return t};return i.d(e,"a",e),e},i.o=function(t,e){return Object.prototype.hasOwnProperty.call(t,e)},i.p="/MarketGuru/";var s=window["webpackJsonp"]=window["webpackJsonp"]||[],c=s.push.bind(s);s.push=e,s=s.slice();for(var l=0;l<s.length;l++)e(s[l]);var u=c;o.push([0,"chunk-vendors"]),a()})({0:function(t,e,a){t.exports=a("56d7")},"56d7":function(t,e,a){"use strict";a.r(e);a("e260"),a("e6cf"),a("cca6"),a("a79d");var r=a("2b0e"),n=function(){var t=this,e=t.$createElement,a=t._self._c||e;return a("v-app",[a("v-app-bar",{attrs:{app:"",color:"primary"}},[a("v-toolbar-title",{staticClass:"white--text",attrs:{dark:""}},[t._v("Welcome to Market Guru ! ")]),a("v-spacer")],1),a("v-main",[a("ErrorNotifications"),a("v-container",{attrs:{fluid:""}},[a("v-container",[a("v-form",{ref:"form",attrs:{"lazy-validation":""},on:{submit:function(t){t.preventDefault()}},model:{value:t.valid,callback:function(e){t.valid=e},expression:"valid"}},[a("v-row",[a("v-text-field",{attrs:{rules:t.ruleTest,required:""},scopedSlots:t._u([{key:"label",fn:function(){return[t._v(" Enter a stock symbol: ")]},proxy:!0}]),model:{value:t.ticker,callback:function(e){t.ticker=e},expression:"ticker"}})],1),a("v-row",{attrs:{align:"center",justify:"center"}},[this.loading?t._e():a("v-btn",{attrs:{disabled:!t.valid,outlined:""},on:{click:t.getRecommendation}},[t._v("Get recommendation")]),this.loading&&!this.stock.displayName?a("v-progress-linear",{attrs:{indeterminate:"",rounded:"",height:"6"}}):t._e()],1)],1)],1),a("StockInformation"),a("StockPriceTable")],1)],1),a("v-footer",{attrs:{color:"primary"}},[a("v-col",{staticClass:"text-center",attrs:{cols:"12",white:""}},[t._v(" "+t._s((new Date).getFullYear())+" — Market Guru ")])],1)],1)},o=[],i=a("1da1"),s=a("5530"),c=(a("96cf"),a("2f62")),l=function(){var t=this,e=t.$createElement,a=t._self._c||e;return a("v-container",[this.stock.displayName?a("v-card",{attrs:{outlined:"",tile:"",loading:this.loading}},[a("v-card-title",[t._v("Monthly price table: ")]),a("v-layout",{attrs:{"justify-center":""}},[a("v-data-table",{staticClass:"elevation-1",attrs:{fluid:"",headers:t.headers,items:t.tableData,"items-per-page":5},scopedSlots:t._u([{key:"item.trend",fn:function(e){var r=e.item;return[a("v-icon",{attrs:{color:r.color,"x-large":""}},[t._v(t._s(r.trend))])]}},{key:"item.timestamp",fn:function(e){var r=e.item;return[a("span",[t._v(t._s(new Date(r.timestamp).toDateString()))])]}},{key:"item.closingPrice",fn:function(e){var r=e.item;return[a("span",{style:{color:r.color}},[t._v(" "+t._s(r.closingPrice))])]}}],null,!1,1422361751)})],1)],1):t._e()],1)},u=[],d=(a("d81d"),{name:"StockPriceTable",components:{},data:function(){return{tableData:[],headers:[{text:"Timestamp",value:"timestamp"},{text:"High",value:"high"},{text:"Low",value:"low"},{text:"Value",value:"closingPrice"},{text:"Trend",value:"trend",sortable:!1}]}},watch:{history:function(t){t.history?this.tableData=t.history.map((function(t,e,a){var r="",n="green",o=a[e+1];return o&&(t.closingPrice>o.closingPrice?(r="mdi-trending-up",n="green"):(r="mdi-trending-down",n="red")),Object(s["a"])(Object(s["a"])({},t),{},{trend:r,color:n})})):this.tableData=[]}},computed:Object(s["a"])({},Object(c["c"])(["stock","history","loading"])),mounted:function(){},methods:{}}),v=d,m=a("2877"),p=a("6544"),f=a.n(p),h=a("b0af"),b=a("99d9"),g=a("a523"),k=a("8fea"),_=a("132d"),y=a("a722"),w=Object(m["a"])(v,l,u,!1,null,null,null),V=w.exports;f()(w,{VCard:h["a"],VCardTitle:b["b"],VContainer:g["a"],VDataTable:k["a"],VIcon:_["a"],VLayout:y["a"]});var x=function(){var t=this,e=t.$createElement,a=t._self._c||e;return a("v-container",[this.stock.displayName?a("v-card",{attrs:{outlined:"",tile:"",loading:this.loading}},[a("v-card-title",[t._v("Stock Information: "),a("v-spacer"),t._v(" "+t._s(this.stock.displayName)+" ("+t._s(this.stock.ticker)+") ")],1),a("v-card-text",[a("v-row",{attrs:{"no-gutters":""}},[a("v-col",{attrs:{align:"center",cols:"12",sm:"4"}},[a("strong",[t._v(" Closing Price:")]),a("v-spacer"),t._v(t._s(this.stock.closingPrice)+" ")],1),a("v-col",{attrs:{align:"center",cols:"12",sm:"4"}},[a("strong",[t._v("Daily high: ")]),a("v-spacer"),t._v(t._s(this.stock.dailyHigh)+" ")],1),a("v-col",{attrs:{align:"center",cols:"12",sm:"4"}},[a("strong",[t._v(" Daily low:")]),a("v-spacer"),t._v(t._s(this.stock.dailyLow)+" ")],1)],1)],1),a("v-divider"),a("v-card-text",[a("v-list",{attrs:{"two-line":""}},[a("v-list-item",[a("v-list-item-content",[a("v-list-item-title",[t._v("Recommendation")]),a("v-list-item-subtitle",[t._v(t._s(this.recommendation.reason))])],1),a("v-list-item-icon",[a("v-chip",[t._v(t._s(this.recommendation.recommendation)+" ")])],1)],1),a("v-divider"),a("v-list-item",[a("v-list-item-content",[a("v-list-item-title",[t._v("Yearly trend:")]),a("v-list-item-subtitle",[t._v(t._s(t.sparklineData[0].year)+" - "+t._s(t.sparklineData[t.sparklineData.length-1].year))]),a("v-sheet",{staticClass:"pa-3 ma-6",attrs:{color:"rgba(0, 0, 0, .12)"}},[this.sparklineData?a("v-sparkline",{attrs:{value:t.sparklineData.map((function(t){return t.value})),"stroke-linecap":"round",height:"100",padding:"24",smooth:""},scopedSlots:t._u([{key:"label",fn:function(e){return[t._v(" $"+t._s(e.value)+" ")]}}],null,!1,3378650747)}):t._e()],1)],1)],1)],1)],1)],1):t._e()],1)},j=[],O=(a("fb6a"),a("4de4"),{name:"StockRecommendation",components:{},data:function(){return{sparklineData:[]}},watch:{history:function(t){t.history?this.sparklineData=t.history.filter((function(t){var e=new Date(t.timestamp);return 0==e.getMonth()})).map((function(t){return{value:t.closingPrice,year:new Date(t.timestamp).getFullYear()}})).slice(0,5).reverse():this.sparklineData=[]}},computed:Object(s["a"])({},Object(c["c"])(["stock","recommendation","history","loading"])),mounted:function(){},methods:{}}),S=O,D=a("cc20"),P=a("62ad"),C=a("ce7e"),T=a("8860"),L=a("da13"),R=a("5d23"),I=a("34c3"),E=a("0fd9"),A=a("8dd9"),M=a("2fa4"),F=a("7f2e"),$=Object(m["a"])(S,x,j,!1,null,null,null),N=$.exports;f()($,{VCard:h["a"],VCardText:b["a"],VCardTitle:b["b"],VChip:D["a"],VCol:P["a"],VContainer:g["a"],VDivider:C["a"],VList:T["a"],VListItem:L["a"],VListItemContent:R["a"],VListItemIcon:I["a"],VListItemSubtitle:R["b"],VListItemTitle:R["c"],VRow:E["a"],VSheet:A["a"],VSpacer:M["a"],VSparkline:F["a"]});var B=function(){var t=this,e=t.$createElement,a=t._self._c||e;return a("v-container",[a("v-snackbar",{attrs:{"multi-line":!0,right:!0,timeout:3500,top:!0,vertical:!0,color:t.snackbar.color},scopedSlots:t._u([{key:"action",fn:function(e){var r=e.attrs;return[a("v-btn",t._b({attrs:{text:""},on:{click:function(e){t.snackbar.show=!1}}},"v-btn",r,!1),[t._v(" Close ")])]}}]),model:{value:t.snackbar.show,callback:function(e){t.$set(t.snackbar,"show",e)},expression:"snackbar.show"}},[a("h3",[t._v(t._s(t.snackbar.event.title))]),a("v-divider",{staticClass:"mx-2"}),t._v(" "+t._s(t.snackbar.event.data.detail)+" "),a("v-divider",{staticClass:"mx-2"})],1)],1)},G=[],Y={name:"SnackBarNotification",components:{},computed:Object(s["a"])({},Object(c["c"])(["error"])),data:function(){return{snackbar:{show:!1,color:null,event:{title:"",data:{}}}}},watch:{error:function(t){var e,a;t.response||t.message?this.snackbar={color:"error",show:!0,event:{title:t,data:null!==(e=null===t||void 0===t||null===(a=t.response)||void 0===a?void 0:a.data)&&void 0!==e?e:t.message}}:console.error(t)}}},H=Y,J=a("8336"),U=a("2db4"),q=Object(m["a"])(H,B,G,!1,null,null,null),z=q.exports;f()(q,{VBtn:J["a"],VContainer:g["a"],VDivider:C["a"],VSnackbar:U["a"]});var W={name:"App",components:{ErrorNotifications:z,StockPriceTable:V,StockInformation:N},data:function(){return{ticker:"",valid:!1,ruleTest:[function(t){return!!t||"Ticker is missing"}]}},computed:Object(s["a"])({},Object(c["c"])(["loading","stock"])),methods:Object(s["a"])({getRecommendation:function(){var t=Object(i["a"])(regeneratorRuntime.mark((function t(){var e;return regeneratorRuntime.wrap((function(t){while(1)switch(t.prev=t.next){case 0:if(e=this.$refs.form.validate(),0!=e){t.next=3;break}return t.abrupt("return");case 3:console.log("Retrieving stock data",this.ticker,e),this.getStockDataFromApi(this.ticker);case 5:case"end":return t.stop()}}),t,this)})));function e(){return t.apply(this,arguments)}return e}()},Object(c["b"])(["getStockDataFromApi"]))},K=W,Q=a("7496"),X=a("40dc"),Z=a("553a"),tt=a("4bd4"),et=a("f6c4"),at=a("8e36"),rt=a("8654"),nt=a("2a7f"),ot=Object(m["a"])(K,n,o,!1,null,null,null),it=ot.exports;f()(ot,{VApp:Q["a"],VAppBar:X["a"],VBtn:J["a"],VCol:P["a"],VContainer:g["a"],VFooter:Z["a"],VForm:tt["a"],VMain:et["a"],VProgressLinear:at["a"],VRow:E["a"],VSpacer:M["a"],VTextField:rt["a"],VToolbarTitle:nt["a"]});var st=a("f309");r["a"].use(st["a"]);var ct=new st["a"]({}),lt=(a("99af"),a("bc3a")),ut=a.n(lt),dt={VUE_APP_API:"https://marketguru-api-3tfjt4473a-uc.a.run.app"};new r["a"];r["a"].use(c["a"]);var vt={stock:{},history:{},recommendation:{},error:{},loading:!1},mt={setResponse:function(t,e){t.history=e.history,t.recommendation=e.recommendation,t.stock=e.stock},removeCurrent:function(t){t.history={},t.recommendation={},t.stock={}},setLoading:function(t,e){t.loading=e},setError:function(t,e){t.error=e}},pt={},ft={getStockDataFromApi:function(t,e){return Object(i["a"])(regeneratorRuntime.mark((function a(){var r;return regeneratorRuntime.wrap((function(a){while(1)switch(a.prev=a.next){case 0:return r=t.commit,r("setLoading",!0),a.next=4,ut.a.get("".concat(dt.VUE_APP_API,"/api/stock/").concat(e)).then((function(t){r("setResponse",t.data)})).catch((function(t){r("removeCurrent"),r("setError",t)}));case 4:r("setLoading",!1);case 5:case"end":return a.stop()}}),a)})))()}},ht=new c["a"].Store({state:vt,getters:pt,actions:ft,mutations:mt});r["a"].config.productionTip=!1,r["a"].use(c["a"]),new r["a"]({vuetify:ct,store:ht,render:function(t){return t(it)}}).$mount("#app")}});
//# sourceMappingURL=app.37af1250.js.map