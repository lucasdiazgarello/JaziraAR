#include "pch-c.h"
#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include "codegen/il2cpp-codegen-metadata.h"





// 0x00000001 System.UInt32 Unity.Networking.QoS.InternalQosResult::get_AverageLatencyMs()
extern void InternalQosResult_get_AverageLatencyMs_mC4DD63FA694AB15B32ECE7B95C391BF79250708B (void);
// 0x00000002 System.Single Unity.Networking.QoS.InternalQosResult::get_PacketLoss()
extern void InternalQosResult_get_PacketLoss_mAE93BDA8F14001F7D8B9B6B07D7BF5A6492F0E7A (void);
// 0x00000003 System.Void Unity.Networking.QoS.InternalQosResult::AddAggregateLatency(System.UInt32)
extern void InternalQosResult_AddAggregateLatency_mF4F807BAA262DEAE0E1B5A741C4D4F3B87AD363F (void);
// 0x00000004 Unity.Jobs.JobHandle Unity.Networking.QoS.QosJob::Schedule(Unity.Jobs.JobHandle)
// 0x00000005 Unity.Collections.NativeArray`1<Unity.Networking.QoS.InternalQosResult> Unity.Networking.QoS.QosJob::get_QosResults()
extern void QosJob_get_QosResults_m99D294F773A497B20A7B607B3A5867AB1DD85CD6 (void);
// 0x00000006 System.Void Unity.Networking.QoS.QosJob::.ctor(System.Collections.Generic.IList`1<Unity.Networking.QoS.UcgQosServer>,System.String,System.UInt32,System.UInt64,System.UInt64,System.UInt32,System.UInt32,System.UInt32)
extern void QosJob__ctor_mC7757F9FE843176A16749879A99C777DD191D4BB (void);
// 0x00000007 System.Void Unity.Networking.QoS.QosJob::Dispose()
extern void QosJob_Dispose_m023B42665979401AB7384B2BAB21B090BDDD21DF (void);
// 0x00000008 System.Void Unity.Networking.QoS.QosJob::Execute()
extern void QosJob_Execute_m66569856598381E3B9D9461F905CD7DD555819C6 (void);
// 0x00000009 System.Void Unity.Networking.QoS.QosJob::ProcessServers(Unity.Baselib.LowLevel.Binding/Baselib_Socket_Handle)
extern void QosJob_ProcessServers_m002469312F12627E8C8179045EF58AAEFC2767B6 (void);
// 0x0000000A System.Void Unity.Networking.QoS.QosJob::ProcessServer(Unity.Networking.QoS.QosJob/InternalQosServer,Unity.Baselib.LowLevel.Binding/Baselib_Socket_Handle)
extern void QosJob_ProcessServer_m193D982E9EBF657DB785605C7ACFDE9FC1BFB121 (void);
// 0x0000000B Unity.Baselib.LowLevel.Binding/Baselib_ErrorCode Unity.Networking.QoS.QosJob::SendQosRequests(Unity.Networking.QoS.QosJob/InternalQosServer,Unity.Baselib.LowLevel.Binding/Baselib_Socket_Handle,Unity.Networking.QoS.InternalQosResult&)
extern void QosJob_SendQosRequests_mE07A62D70704ABCBBFFD5C5B0A9E9D2075DC6A1E (void);
// 0x0000000C System.Void Unity.Networking.QoS.QosJob::StoreServer(Unity.Networking.QoS.QosJob/InternalQosServer)
extern void QosJob_StoreServer_m10EC014D95822A081040752A7F218E8C3A511A09 (void);
// 0x0000000D System.Void Unity.Networking.QoS.QosJob::StoreResult(System.Int32,Unity.Networking.QoS.InternalQosResult)
extern void QosJob_StoreResult_m4A593D9ECAFD18243069F8F1E01DD5362F5CCFC5 (void);
// 0x0000000E System.Void Unity.Networking.QoS.QosJob::RecvQosResponsesTimed(Unity.Networking.QoS.NetworkEndPoint,System.DateTime,Unity.Baselib.LowLevel.Binding/Baselib_Socket_Handle,System.Boolean)
extern void QosJob_RecvQosResponsesTimed_m05465557D4349CA3EB44FFFF643B1C32717301F6 (void);
// 0x0000000F System.Void Unity.Networking.QoS.QosJob::RecvQosResponses(Unity.Networking.QoS.NetworkEndPoint,System.DateTime,Unity.Baselib.LowLevel.Binding/Baselib_Socket_Handle,System.Boolean)
extern void QosJob_RecvQosResponses_mF7D285B242196414A29B2EB92995C41D5EF0E9CC (void);
// 0x00000010 System.String Unity.Networking.QoS.QosJob::EnableReceiveWait()
extern void QosJob_EnableReceiveWait_m0411682DA5665E33783F6DE96212DEB4F369FC43 (void);
// 0x00000011 System.Int32 Unity.Networking.QoS.QosJob::LookupResult(Unity.Networking.QoS.NetworkEndPoint,Unity.Networking.QoS.QosResponse,Unity.Networking.QoS.InternalQosResult&)
extern void QosJob_LookupResult_mF78311CDB65FB597D261547BD872FBBB99D0D43E (void);
// 0x00000012 System.ValueTuple`2<Unity.Baselib.LowLevel.Binding/Baselib_Socket_Handle,Unity.Baselib.LowLevel.Binding/Baselib_ErrorCode> Unity.Networking.QoS.QosJob::CreateAndBindSocket()
extern void QosJob_CreateAndBindSocket_mC2628791199CB10A3C0FCE263504FE8E9E552AD4 (void);
// 0x00000013 System.Void Unity.Networking.QoS.QosJob/InternalQosServer::.ctor(Unity.Networking.QoS.NetworkEndPoint,System.DateTime,System.Int32)
extern void InternalQosServer__ctor_mDFFD17B79B32982F24BBF7814A2238B3A8DC1AA2 (void);
// 0x00000014 System.Int32 Unity.Networking.QoS.QosJob/InternalQosServer::get_FirstIdx()
extern void InternalQosServer_get_FirstIdx_mE1E26F2B9C43302D4D3A669677A32BB4D2CE0478 (void);
// 0x00000015 System.Void Unity.Networking.QoS.QosJob/InternalQosServer::set_FirstIdx(System.Int32)
extern void InternalQosServer_set_FirstIdx_mEE8FDA205B7B8B2DE6E8C6F1D0BFF888C1434A28 (void);
// 0x00000016 System.UInt16 Unity.Networking.QoS.QosJob/InternalQosServer::get_RequestIdentifier()
extern void InternalQosServer_get_RequestIdentifier_m5BB7DCEB91EAD25DBD1733A74B42F4B490CCA2D5 (void);
// 0x00000017 System.Void Unity.Networking.QoS.QosJob/InternalQosServer::set_RequestIdentifier(System.UInt16)
extern void InternalQosServer_set_RequestIdentifier_mAA9EF4C5C34AFB4EA335CBF999D2FBB46BD15E1F (void);
// 0x00000018 System.Boolean Unity.Networking.QoS.QosJob/InternalQosServer::get_Duplicate()
extern void InternalQosServer_get_Duplicate_mC9F0B30BEC3CEC496735613D59181989F13DEF2B (void);
// 0x00000019 System.String Unity.Networking.QoS.QosJob/InternalQosServer::get_Address()
extern void InternalQosServer_get_Address_mAF7047289348E24CA18A4202DA79D49C2E2298E4 (void);
// 0x0000001A System.UInt16 Unity.Networking.QoS.NetworkEndPoint::get_Port()
extern void NetworkEndPoint_get_Port_m251B69FEFF5FC2CF22A30DB9AA40348F340408CB (void);
// 0x0000001B Unity.Networking.QoS.NetworkFamily Unity.Networking.QoS.NetworkEndPoint::get_Family()
extern void NetworkEndPoint_get_Family_mD35537952F0BCF8578F516DC06DC1D3B225F724F (void);
// 0x0000001C System.String Unity.Networking.QoS.NetworkEndPoint::get_Address()
extern void NetworkEndPoint_get_Address_m1EB1D2DAF46FB52253E311B8423DF10FF8F03FBD (void);
// 0x0000001D System.Boolean Unity.Networking.QoS.NetworkEndPoint::get_IsValid()
extern void NetworkEndPoint_get_IsValid_m56A53E05D4C28A38048155E597009964FBAE2D94 (void);
// 0x0000001E System.Boolean Unity.Networking.QoS.NetworkEndPoint::TryParse(System.String,System.UInt16,Unity.Networking.QoS.NetworkEndPoint&,Unity.Networking.QoS.NetworkFamily)
extern void NetworkEndPoint_TryParse_m290522A4D3F16227CB050DAAC84B0F7F9776E873 (void);
// 0x0000001F System.String Unity.Networking.QoS.NetworkEndPoint::AddressAsString()
extern void NetworkEndPoint_AddressAsString_mBF7B5153F6570D362AB7672C08CB24369B78D0E3 (void);
// 0x00000020 Unity.Networking.QoS.NetworkFamily Unity.Networking.QoS.NetworkEndPoint::FromBaselibFamily(Unity.Baselib.LowLevel.Binding/Baselib_NetworkAddress_Family)
extern void NetworkEndPoint_FromBaselibFamily_mF9E814790BF6646253228590A2C5938141A99CA9 (void);
// 0x00000021 Unity.Baselib.LowLevel.Binding/Baselib_NetworkAddress_Family Unity.Networking.QoS.NetworkEndPoint::ToBaselibFamily(Unity.Networking.QoS.NetworkFamily)
extern void NetworkEndPoint_ToBaselibFamily_m0F7E8A742E96033C58E29C69FBC20E3C5849DBB4 (void);
// 0x00000022 System.Boolean Unity.Networking.QoS.QosHelper::WouldBlock(System.UInt64)
extern void QosHelper_WouldBlock_mF00EFCE5355BC0FB84B9707F2B79BB6BD614A12D (void);
// 0x00000023 System.Boolean Unity.Networking.QoS.QosHelper::ExpiredUtc(System.DateTime)
extern void QosHelper_ExpiredUtc_m78BBD5851F2AD5E38E8A7F369BC8B0A0F0241861 (void);
// 0x00000024 System.String Unity.Networking.QoS.QosHelper::Since(System.DateTime)
extern void QosHelper_Since_m21E114D7D7BF2EAE40D75BEF8A64262D7E644EBC (void);
// 0x00000025 System.Byte Unity.Networking.QoS.QosRequest::get_Magic()
extern void QosRequest_get_Magic_m6F6463246F68BE8FCDACAC075DC10B8C08F23439 (void);
// 0x00000026 System.Byte Unity.Networking.QoS.QosRequest::get_Version()
extern void QosRequest_get_Version_mC41475C20C86D26270BBA934AA806FCD8A910238 (void);
// 0x00000027 System.Byte Unity.Networking.QoS.QosRequest::get_FlowControl()
extern void QosRequest_get_FlowControl_m8B743B201483EC39C6F7E57579F36D841F276D03 (void);
// 0x00000028 System.Byte[] Unity.Networking.QoS.QosRequest::get_Title()
extern void QosRequest_get_Title_m9E68CBB84A55BD2B1E05F4943910C91A3ABE8236 (void);
// 0x00000029 System.Void Unity.Networking.QoS.QosRequest::set_Title(System.Byte[])
extern void QosRequest_set_Title_m835EE28AF9C6C4C8B30DE80EB01A61DFB27E52C2 (void);
// 0x0000002A System.Byte Unity.Networking.QoS.QosRequest::get_Sequence()
extern void QosRequest_get_Sequence_mB7BD623CF4FF0ADC6F36BBA4A5F83E5F4BB7892E (void);
// 0x0000002B System.Void Unity.Networking.QoS.QosRequest::set_Sequence(System.Byte)
extern void QosRequest_set_Sequence_m86A820A182DC7991C00DFB052A0130426633BEF7 (void);
// 0x0000002C System.UInt16 Unity.Networking.QoS.QosRequest::get_Identifier()
extern void QosRequest_get_Identifier_m5F1968614F882703F1BF258B7F920A4B43D8D062 (void);
// 0x0000002D System.Void Unity.Networking.QoS.QosRequest::set_Identifier(System.UInt16)
extern void QosRequest_set_Identifier_mBAA98E7B4ED9E4BC28CC5E29ADF69FE96C4987CF (void);
// 0x0000002E System.UInt64 Unity.Networking.QoS.QosRequest::get_Timestamp()
extern void QosRequest_get_Timestamp_mA705858832F72986788EAE43B411543CC563F5FD (void);
// 0x0000002F System.Void Unity.Networking.QoS.QosRequest::set_Timestamp(System.UInt64)
extern void QosRequest_set_Timestamp_m713A87D705A188F63BE58B901E51839C90214801 (void);
// 0x00000030 System.Int32 Unity.Networking.QoS.QosRequest::get_Length()
extern void QosRequest_get_Length_mAA37DB5D869FE6DDAEEA9F5AD47E9109445A6853 (void);
// 0x00000031 System.ValueTuple`2<System.UInt32,System.Int32> Unity.Networking.QoS.QosRequest::Send(System.IntPtr,Unity.Networking.QoS.NetworkEndPoint,System.DateTime)
extern void QosRequest_Send_m29F081B6A23999A4F122B039D6843AB14B6B4C0E (void);
// 0x00000032 System.Void Unity.Networking.QoS.QosRequest::.ctor()
extern void QosRequest__ctor_m6DC94F804A906E394B07229DB0F6673A1D615395 (void);
// 0x00000033 System.Byte Unity.Networking.QoS.QosResponse::get_Magic()
extern void QosResponse_get_Magic_m02CCA49C37D883E7F76ECCB379DF3FBC7A9212D2 (void);
// 0x00000034 System.Byte Unity.Networking.QoS.QosResponse::get_Version()
extern void QosResponse_get_Version_mA10B81D558000ED037D5C5B645E88FFD33A05747 (void);
// 0x00000035 System.Byte Unity.Networking.QoS.QosResponse::get_FlowControl()
extern void QosResponse_get_FlowControl_mF09A6641107229409D421F7222B2B0BD2EDD677C (void);
// 0x00000036 System.Byte Unity.Networking.QoS.QosResponse::get_Sequence()
extern void QosResponse_get_Sequence_m28659E53C16DABD437F1B3BFA4CDC600D86C6D5E (void);
// 0x00000037 System.UInt16 Unity.Networking.QoS.QosResponse::get_Identifier()
extern void QosResponse_get_Identifier_mCEB0525F3D9FF3579526AAFA0D7B225D70BE927B (void);
// 0x00000038 System.UInt64 Unity.Networking.QoS.QosResponse::get_Timestamp()
extern void QosResponse_get_Timestamp_mB9E55DA94DCCBC222AA6C8B74DCE71BD40FF5169 (void);
// 0x00000039 System.UInt16 Unity.Networking.QoS.QosResponse::get_Length()
extern void QosResponse_get_Length_m0B242FFD9866C8C2E8312D323377912462342581 (void);
// 0x0000003A System.Int32 Unity.Networking.QoS.QosResponse::get_LatencyMs()
extern void QosResponse_get_LatencyMs_m6D92DA16A56CC151961BCC6E090131C2097BF5FC (void);
// 0x0000003B System.ValueTuple`2<System.Int32,System.Int32> Unity.Networking.QoS.QosResponse::Recv(System.IntPtr,System.Boolean,System.DateTime,Unity.Networking.QoS.NetworkEndPoint&)
extern void QosResponse_Recv_mC619EA992F44794C22AE151014FAAA8B5CE0E204 (void);
// 0x0000003C System.Boolean Unity.Networking.QoS.QosResponse::Verify(System.UInt32,System.String&)
extern void QosResponse_Verify_mCC3F2D873A922CFD8A9DE72F64E3D20F1958E369 (void);
// 0x0000003D System.ValueTuple`2<Unity.Networking.QoS.FcType,System.Byte> Unity.Networking.QoS.QosResponse::ParseFlowControl()
extern void QosResponse_ParseFlowControl_mA247FCC942E7322C2CC02B3584FF871AEB0D4F2B (void);
// 0x0000003E System.Void Unity.Networking.QoS.QosResponse::.ctor()
extern void QosResponse__ctor_m209C46EAA2D73534740EDB232775F5E32E8DBA4D (void);
// 0x0000003F System.String Unity.Networking.QoS.UcgQosServer::ToString()
extern void UcgQosServer_ToString_mA7BEF62CE3005CD01054E082587C558D6C031AE8 (void);
// 0x00000040 System.Void Unity.Services.Qos.Configuration::.ctor(System.String,System.Nullable`1<System.Int32>,System.Nullable`1<System.Int32>,System.Collections.Generic.IDictionary`2<System.String,System.String>)
extern void Configuration__ctor_mFF1BCF6303D8E5E362C3655F302DD9E8870BCD33 (void);
// 0x00000041 Unity.Services.Qos.Configuration Unity.Services.Qos.Configuration::MergeConfigurations(Unity.Services.Qos.Configuration,Unity.Services.Qos.Configuration)
extern void Configuration_MergeConfigurations_m10AD4EC118B0BF363E4C1993A490032DC0CE981D (void);
// 0x00000042 System.Void Unity.Services.Qos.QosResults::.ctor(Unity.Services.Qos.WrappedQosService)
extern void QosResults__ctor_m6AD651FDED7000ABD5E4B91076A06D599A64F78B (void);
// 0x00000043 System.Threading.Tasks.Task`1<System.Collections.Generic.IList`1<Unity.Services.Qos.Internal.QosResult>> Unity.Services.Qos.QosResults::GetSortedQosResultsAsync(System.String,System.Collections.Generic.IList`1<System.String>)
extern void QosResults_GetSortedQosResultsAsync_mFEBE0655FD611148054C48B89D5909160C1F5311 (void);
// 0x00000044 System.Collections.Generic.Dictionary`2<System.String,System.String> Unity.Services.Qos.Response::get_Headers()
extern void Response_get_Headers_m61DA9ECA3129CE732D2359F5B59B9BFBA86E6744 (void);
// 0x00000045 System.Int64 Unity.Services.Qos.Response::get_Status()
extern void Response_get_Status_mE999C886D428C9253900FE67E14827DCB10D8B01 (void);
// 0x00000046 System.Void Unity.Services.Qos.Response::set_Status(System.Int64)
extern void Response_set_Status_mBF047132804F44DADB37CCA4D2A5CDB3602AAC79 (void);
// 0x00000047 System.Void Unity.Services.Qos.Response::.ctor(Unity.Services.Qos.Http.HttpClientResponse)
extern void Response__ctor_mC03DB957D3366BECC35CB74B16E0756A07E6CB55 (void);
// 0x00000048 T Unity.Services.Qos.Response`1::get_Result()
// 0x00000049 System.Void Unity.Services.Qos.Response`1::.ctor(Unity.Services.Qos.Http.HttpClientResponse,T)
// 0x0000004A Unity.Services.Qos.IQosDiscoveryService Unity.Services.Qos.QosDiscoveryService::get_Instance()
extern void QosDiscoveryService_get_Instance_m0BC7CD7D6BE39F77087A5B7322D7D3D69F012483 (void);
// 0x0000004B System.Void Unity.Services.Qos.QosDiscoveryService::set_Instance(Unity.Services.Qos.IQosDiscoveryService)
extern void QosDiscoveryService_set_Instance_m67A235B23405CEEC8D9B28055A34B18A089E81AA (void);
// 0x0000004C Unity.Services.Qos.Apis.QosDiscovery.IQosDiscoveryApiClient Unity.Services.Qos.IQosDiscoveryService::get_QosDiscoveryApi()
// 0x0000004D System.Void Unity.Services.Qos.IQosDiscoveryService::set_QosDiscoveryApi(Unity.Services.Qos.Apis.QosDiscovery.IQosDiscoveryApiClient)
// 0x0000004E Unity.Services.Qos.Configuration Unity.Services.Qos.IQosDiscoveryService::get_Configuration()
// 0x0000004F System.Void Unity.Services.Qos.IQosDiscoveryService::set_Configuration(Unity.Services.Qos.Configuration)
// 0x00000050 System.Void Unity.Services.Qos.QosPackageInitializer::Register()
extern void QosPackageInitializer_Register_m296754B1BE6F04E0FDCE27B11D8FAD3F660ACD70 (void);
// 0x00000051 System.Void Unity.Services.Qos.QosPackageInitializer::Register(Unity.Services.Core.Internal.CoreRegistry)
extern void QosPackageInitializer_Register_m03F66F4C3694C6000ACFE631EB2AA0C3B8B741D2 (void);
// 0x00000052 System.Threading.Tasks.Task Unity.Services.Qos.QosPackageInitializer::Initialize(Unity.Services.Core.Internal.CoreRegistry)
extern void QosPackageInitializer_Initialize_m31AE4939EEC42056FBC52EB1885A66161909312C (void);
// 0x00000053 System.String Unity.Services.Qos.QosPackageInitializer::GetHost(Unity.Services.Core.Configuration.Internal.IProjectConfiguration)
extern void QosPackageInitializer_GetHost_m411B2841F662CD20FE22DB9F61BCD9CE392563FA (void);
// 0x00000054 System.Void Unity.Services.Qos.QosPackageInitializer::.ctor()
extern void QosPackageInitializer__ctor_mB0FBE994896B3205A0F3F31493AB2958391C088A (void);
// 0x00000055 System.Void Unity.Services.Qos.InternalQosDiscoveryService::.ctor(System.String,Unity.Services.Qos.Http.HttpClient,Unity.Services.Authentication.Internal.IAccessToken)
extern void InternalQosDiscoveryService__ctor_m4B669E8B21EB913A51D88F1250D898A326EC08A7 (void);
// 0x00000056 Unity.Services.Qos.Apis.QosDiscovery.IQosDiscoveryApiClient Unity.Services.Qos.InternalQosDiscoveryService::get_QosDiscoveryApi()
extern void InternalQosDiscoveryService_get_QosDiscoveryApi_mD69672FD2DB905D120F004AC9053987010DDC3A0 (void);
// 0x00000057 System.Void Unity.Services.Qos.InternalQosDiscoveryService::set_QosDiscoveryApi(Unity.Services.Qos.Apis.QosDiscovery.IQosDiscoveryApiClient)
extern void InternalQosDiscoveryService_set_QosDiscoveryApi_m11DE640172B48DF8EBFB3F82808D795F41C6E524 (void);
// 0x00000058 Unity.Services.Qos.Configuration Unity.Services.Qos.InternalQosDiscoveryService::get_Configuration()
extern void InternalQosDiscoveryService_get_Configuration_mA0DD4210EE91DC1A3D859A9739905D7A927F60D0 (void);
// 0x00000059 System.Void Unity.Services.Qos.InternalQosDiscoveryService::set_Configuration(Unity.Services.Qos.Configuration)
extern void InternalQosDiscoveryService_set_Configuration_m217D3AA86C388DD9373322CE07C7393FC1832264 (void);
// 0x0000005A Unity.Services.Qos.IQosService Unity.Services.Qos.QosService::get_Instance()
extern void QosService_get_Instance_m7CE84EAAA9BFA0CE64E1C1C114A669ED706243FC (void);
// 0x0000005B System.Void Unity.Services.Qos.QosService::set_Instance(Unity.Services.Qos.IQosService)
extern void QosService_set_Instance_m10C3C8DF77A585F6F3A45EC8320155D2B98BFCA0 (void);
// 0x0000005C System.Threading.Tasks.Task`1<System.Collections.Generic.IList`1<Unity.Services.Qos.IQosResult>> Unity.Services.Qos.IQosService::GetSortedQosResultsAsync(System.String,System.Collections.Generic.IList`1<System.String>)
// 0x0000005D System.String Unity.Services.Qos.IQosResult::get_Region()
// 0x0000005E System.Int32 Unity.Services.Qos.IQosResult::get_AverageLatencyMs()
// 0x0000005F System.Single Unity.Services.Qos.IQosResult::get_PacketLossPercent()
// 0x00000060 System.Void Unity.Services.Qos.WrappedQosService::.ctor(Unity.Services.Qos.Apis.QosDiscovery.IQosDiscoveryApiClient,Unity.Services.Qos.Runner.IQosRunner,Unity.Services.Authentication.Internal.IAccessToken,Unity.Services.Core.Telemetry.Internal.IMetrics)
extern void WrappedQosService__ctor_m81A14C7E7AA366C58E9FFF014828EBD61FF11CBA (void);
// 0x00000061 System.Threading.Tasks.Task`1<System.Collections.Generic.IList`1<Unity.Services.Qos.IQosResult>> Unity.Services.Qos.WrappedQosService::GetSortedQosResultsAsync(System.String,System.Collections.Generic.IList`1<System.String>)
extern void WrappedQosService_GetSortedQosResultsAsync_m6FF38AB3A4BE3D3C81822413DAF34E4E2C356DEF (void);
// 0x00000062 System.Threading.Tasks.Task`1<System.Collections.Generic.IList`1<Unity.Services.Qos.Internal.QosResult>> Unity.Services.Qos.WrappedQosService::GetSortedInternalQosResultsAsync(System.String,System.Collections.Generic.IList`1<System.String>)
extern void WrappedQosService_GetSortedInternalQosResultsAsync_mA47A99113119329B564320BFAB1325B490670F57 (void);
// 0x00000063 System.Collections.Generic.List`1<Unity.Services.Qos.Internal.QosResult> Unity.Services.Qos.WrappedQosService::SortResults(System.Collections.Generic.IList`1<Unity.Services.Qos.Internal.QosResult>)
extern void WrappedQosService_SortResults_m2AC6413067175DCF1399CF82570FEBEBFCD90890 (void);
// 0x00000064 System.Void Unity.Services.Qos.WrappedQosService::SendResultMetrics(System.Collections.Generic.IList`1<Unity.Services.Qos.Internal.QosResult>,System.String,Unity.Services.Qos.Response)
extern void WrappedQosService_SendResultMetrics_mF81ACBE0E468F70FC816FBECBC61FB76596CF071 (void);
// 0x00000065 Unity.Services.Qos.IQosResult Unity.Services.Qos.WrappedQosService::MapToPublicQosResult(Unity.Services.Qos.Internal.QosResult)
extern void WrappedQosService_MapToPublicQosResult_mE680B3A1FF2741E4DA4663D37BD0DD5602AF3235 (void);
// 0x00000066 System.Void Unity.Services.Qos.WrappedQosService/<GetSortedQosResultsAsync>d__13::MoveNext()
extern void U3CGetSortedQosResultsAsyncU3Ed__13_MoveNext_m3A1785964F213FD44A0500D6EFAAD84E54A76E66 (void);
// 0x00000067 System.Void Unity.Services.Qos.WrappedQosService/<GetSortedQosResultsAsync>d__13::SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine)
extern void U3CGetSortedQosResultsAsyncU3Ed__13_SetStateMachine_m299AEC49C87C4A3498650B4954D477D2EF83F34A (void);
// 0x00000068 System.Void Unity.Services.Qos.WrappedQosService/<GetSortedInternalQosResultsAsync>d__14::MoveNext()
extern void U3CGetSortedInternalQosResultsAsyncU3Ed__14_MoveNext_m5CF9319E26D43DB9FE32CB0887CF6B212BF37B01 (void);
// 0x00000069 System.Void Unity.Services.Qos.WrappedQosService/<GetSortedInternalQosResultsAsync>d__14::SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine)
extern void U3CGetSortedInternalQosResultsAsyncU3Ed__14_SetStateMachine_mC8A0A6BEADA8763C7869554817A2D82D3826EEDB (void);
// 0x0000006A System.Void Unity.Services.Qos.WrappedQosService/<>c::.cctor()
extern void U3CU3Ec__cctor_mAA0A9893E2022F1D0029C2E7A8EF3F3D1BB80548 (void);
// 0x0000006B System.Void Unity.Services.Qos.WrappedQosService/<>c::.ctor()
extern void U3CU3Ec__ctor_m5D76CBDAAD00C935B111C3EF84C85770C592F4AB (void);
// 0x0000006C System.Int32 Unity.Services.Qos.WrappedQosService/<>c::<SortResults>b__15_0(Unity.Services.Qos.Internal.QosResult)
extern void U3CU3Ec_U3CSortResultsU3Eb__15_0_m2B8F7BB79D34848D520BDABEBCC8E5709A83CFC4 (void);
// 0x0000006D System.Single Unity.Services.Qos.WrappedQosService/<>c::<SortResults>b__15_1(Unity.Services.Qos.Internal.QosResult)
extern void U3CU3Ec_U3CSortResultsU3Eb__15_1_mFA4B96B7622491ABAE392DEED2609383BFD1AD9C (void);
// 0x0000006E System.Void Unity.Services.Qos.QosResult::.ctor(System.String,System.Int32,System.Single)
extern void QosResult__ctor_mA7BAE5428A353A31F1F4E9424A78B3FC99706C9C (void);
// 0x0000006F System.String Unity.Services.Qos.QosResult::get_Region()
extern void QosResult_get_Region_mF3A9270B69D29876DDCB738238875CB8FBEF357D (void);
// 0x00000070 System.Int32 Unity.Services.Qos.QosResult::get_AverageLatencyMs()
extern void QosResult_get_AverageLatencyMs_mD768392DF931372CFEADC93BFDB4F3402E83884E (void);
// 0x00000071 System.Single Unity.Services.Qos.QosResult::get_PacketLossPercent()
extern void QosResult_get_PacketLossPercent_m2EE3AABDE7587BEB9CEFEBB352BD23A98F3E1B6F (void);
// 0x00000072 System.Void Unity.Services.Qos.UnsupportedEditorVersionException::.ctor()
extern void UnsupportedEditorVersionException__ctor_m8647299E324884409566642AEEEC36729FA5F07A (void);
// 0x00000073 System.Void Unity.Services.Qos.UnsupportedEditorVersionException::.ctor(System.String)
extern void UnsupportedEditorVersionException__ctor_mC11FE9277AF1738CAF4411DBD37F10AA9D08B5AD (void);
// 0x00000074 System.Void Unity.Services.Qos.Scheduler.EngineStateHelper::Init()
extern void EngineStateHelper_Init_mCD239B6552B997DD76949AEF864CF813A6BDB6FD (void);
// 0x00000075 System.Threading.SynchronizationContext Unity.Services.Qos.Scheduler.ThreadHelper::get_SynchronizationContext()
extern void ThreadHelper_get_SynchronizationContext_mEBBC5F6DC87B64616443CEC47B262855D07E3F3F (void);
// 0x00000076 System.Threading.Tasks.TaskScheduler Unity.Services.Qos.Scheduler.ThreadHelper::get_TaskScheduler()
extern void ThreadHelper_get_TaskScheduler_mE4BB459B45461BD9314E69B218A9D3F021E98672 (void);
// 0x00000077 System.Int32 Unity.Services.Qos.Scheduler.ThreadHelper::get_MainThreadId()
extern void ThreadHelper_get_MainThreadId_mCD0E6B56C919EE955A6854D09DE35BDD964B4027 (void);
// 0x00000078 System.Void Unity.Services.Qos.Scheduler.ThreadHelper::Init()
extern void ThreadHelper_Init_m06C176E2197E46ADA5299D3B56CB0DAF6A996525 (void);
// 0x00000079 System.Void Unity.Services.Qos.Runner.QosJobProvider::.ctor(System.Object,System.IntPtr)
extern void QosJobProvider__ctor_mF18C33B6615A7DB6BB7D499EBF39C5A4C0E6BA86 (void);
// 0x0000007A Unity.Services.Qos.Runner.IQosJob Unity.Services.Qos.Runner.QosJobProvider::Invoke(System.Collections.Generic.IList`1<Unity.Networking.QoS.UcgQosServer>,System.String)
extern void QosJobProvider_Invoke_mCB0FA4045A9C73CF90E75AD05B0941BFBB502A7E (void);
// 0x0000007B System.IAsyncResult Unity.Services.Qos.Runner.QosJobProvider::BeginInvoke(System.Collections.Generic.IList`1<Unity.Networking.QoS.UcgQosServer>,System.String,System.AsyncCallback,System.Object)
extern void QosJobProvider_BeginInvoke_mE3401CEFB56B3A3966ECA906FACBB7521970B7D4 (void);
// 0x0000007C Unity.Services.Qos.Runner.IQosJob Unity.Services.Qos.Runner.QosJobProvider::EndInvoke(System.IAsyncResult)
extern void QosJobProvider_EndInvoke_mC04093465A5A7837DDD88EF683C5442E0CE38FA9 (void);
// 0x0000007D System.Void Unity.Services.Qos.Runner.DnsResolver::.ctor(System.Object,System.IntPtr)
extern void DnsResolver__ctor_m8190EBA2601E2FE2EB896BA1B4F3D1C26BF5DB15 (void);
// 0x0000007E System.Threading.Tasks.Task`1<System.Net.IPAddress[]> Unity.Services.Qos.Runner.DnsResolver::Invoke(System.String)
extern void DnsResolver_Invoke_m6BD09E67240C78EF4CEEBA58370775BE7B198520 (void);
// 0x0000007F System.IAsyncResult Unity.Services.Qos.Runner.DnsResolver::BeginInvoke(System.String,System.AsyncCallback,System.Object)
extern void DnsResolver_BeginInvoke_mF04E381AFD35B6CBCEB317025B52F7EC63FDE8A4 (void);
// 0x00000080 System.Threading.Tasks.Task`1<System.Net.IPAddress[]> Unity.Services.Qos.Runner.DnsResolver::EndInvoke(System.IAsyncResult)
extern void DnsResolver_EndInvoke_mEDC9BF3A6C20588EE374D639BFDC4A01EB3839A2 (void);
// 0x00000081 System.Void Unity.Services.Qos.Runner.BaselibQosRunner::.ctor(Unity.Services.Qos.Runner.QosJobProvider,Unity.Services.Qos.Runner.DnsResolver)
extern void BaselibQosRunner__ctor_mD170788D00E9722B9479E4FF4440D4161A4D43E6 (void);
// 0x00000082 System.Threading.Tasks.Task`1<System.Collections.Generic.List`1<Unity.Services.Qos.Internal.QosResult>> Unity.Services.Qos.Runner.BaselibQosRunner::MeasureQosAsync(System.Collections.Generic.IList`1<Unity.Services.Qos.Models.QosServer>)
extern void BaselibQosRunner_MeasureQosAsync_m6EDEEF2BA140E318C908BF8DF5CC1BA194AF6DB8 (void);
// 0x00000083 System.Threading.Tasks.Task`1<System.Nullable`1<Unity.Networking.QoS.UcgQosServer>> Unity.Services.Qos.Runner.BaselibQosRunner::ToUcgFormat(Unity.Services.Qos.Models.QosServer)
extern void BaselibQosRunner_ToUcgFormat_m1419968ECB7ECDB7B32C74DB6CBBF1CF61B2FA89 (void);
// 0x00000084 System.Collections.Generic.List`1<Unity.Services.Qos.Internal.QosResult> Unity.Services.Qos.Runner.BaselibQosRunner::ParseResults(System.Collections.Generic.IEnumerable`1<Unity.Networking.QoS.InternalQosResult>,System.Collections.Generic.IEnumerable`1<Unity.Services.Qos.Models.QosServer>)
extern void BaselibQosRunner_ParseResults_mF971BD8AA0A60F083F10C21B1FBB6322B23FA80A (void);
// 0x00000085 System.Void Unity.Services.Qos.Runner.BaselibQosRunner/<>c::.cctor()
extern void U3CU3Ec__cctor_m26C8EDF69C0D13638AD94C6488ED58BD79C95C7F (void);
// 0x00000086 System.Void Unity.Services.Qos.Runner.BaselibQosRunner/<>c::.ctor()
extern void U3CU3Ec__ctor_m53A1925C62020D1466BB7427BA0A020BD5E7B040 (void);
// 0x00000087 Unity.Services.Qos.Runner.IQosJob Unity.Services.Qos.Runner.BaselibQosRunner/<>c::<.ctor>b__2_0(System.Collections.Generic.IList`1<Unity.Networking.QoS.UcgQosServer>,System.String)
extern void U3CU3Ec_U3C_ctorU3Eb__2_0_mF20635D6BECBC77DD68D7933D34C74E53BF39651 (void);
// 0x00000088 System.Boolean Unity.Services.Qos.Runner.BaselibQosRunner/<>c::<MeasureQosAsync>b__3_0(System.Nullable`1<Unity.Networking.QoS.UcgQosServer>)
extern void U3CU3Ec_U3CMeasureQosAsyncU3Eb__3_0_mEF244A574C993EC47234D5203EBF0F9A2D32A2A0 (void);
// 0x00000089 Unity.Networking.QoS.UcgQosServer Unity.Services.Qos.Runner.BaselibQosRunner/<>c::<MeasureQosAsync>b__3_1(System.Nullable`1<Unity.Networking.QoS.UcgQosServer>)
extern void U3CU3Ec_U3CMeasureQosAsyncU3Eb__3_1_mDDF2659E48C4F06482DCC87A0908A9A19C2168C2 (void);
// 0x0000008A System.Void Unity.Services.Qos.Runner.BaselibQosRunner/<MeasureQosAsync>d__3::MoveNext()
extern void U3CMeasureQosAsyncU3Ed__3_MoveNext_mDCDD25826B3494B3AE4241AE8A3AD5F8DFB2A29C (void);
// 0x0000008B System.Void Unity.Services.Qos.Runner.BaselibQosRunner/<MeasureQosAsync>d__3::SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine)
extern void U3CMeasureQosAsyncU3Ed__3_SetStateMachine_m1D0DB6B0A212BD39D5B0D6E603F6DD02825170EE (void);
// 0x0000008C System.Void Unity.Services.Qos.Runner.BaselibQosRunner/<ToUcgFormat>d__4::MoveNext()
extern void U3CToUcgFormatU3Ed__4_MoveNext_m0ECDBA162DD2D83E2B015AD598F48967099F2B98 (void);
// 0x0000008D System.Void Unity.Services.Qos.Runner.BaselibQosRunner/<ToUcgFormat>d__4::SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine)
extern void U3CToUcgFormatU3Ed__4_SetStateMachine_mF1C11985D692E30EBB0C4B9802D4CA064A023CEB (void);
// 0x0000008E Unity.Jobs.JobHandle Unity.Services.Qos.Runner.IQosJob::Schedule(Unity.Jobs.JobHandle)
// 0x0000008F System.Void Unity.Services.Qos.Runner.IQosJob::Dispose()
// 0x00000090 Unity.Collections.NativeArray`1<Unity.Networking.QoS.InternalQosResult> Unity.Services.Qos.Runner.IQosJob::get_QosResults()
// 0x00000091 System.Threading.Tasks.Task`1<System.Collections.Generic.List`1<Unity.Services.Qos.Internal.QosResult>> Unity.Services.Qos.Runner.IQosRunner::MeasureQosAsync(System.Collections.Generic.IList`1<Unity.Services.Qos.Models.QosServer>)
// 0x00000092 System.Void Unity.Services.Qos.Models.ErrorResponseBody::.ctor(System.String,System.String,System.Int32,System.Int32,System.String,System.String,System.Collections.Generic.List`1<System.Object>)
extern void ErrorResponseBody__ctor_mD1405A130D7BC0164F06C0B1431A3E555CA39800 (void);
// 0x00000093 System.String Unity.Services.Qos.Models.ErrorResponseBody::get_Type()
extern void ErrorResponseBody_get_Type_mEEF34D6A7A21C9D856E2658F65E38CF8EB2679BA (void);
// 0x00000094 System.String Unity.Services.Qos.Models.ErrorResponseBody::get_Title()
extern void ErrorResponseBody_get_Title_mFAAD11466E8D4379B8D5A6886728D125963E866A (void);
// 0x00000095 System.Int32 Unity.Services.Qos.Models.ErrorResponseBody::get_Status()
extern void ErrorResponseBody_get_Status_m875015E553B6BCD878A780B184DFC037B4709807 (void);
// 0x00000096 System.Int32 Unity.Services.Qos.Models.ErrorResponseBody::get_Code()
extern void ErrorResponseBody_get_Code_mDB11C04F90C5B4F4EB37657ED8DC999A1002D100 (void);
// 0x00000097 System.String Unity.Services.Qos.Models.ErrorResponseBody::get_Detail()
extern void ErrorResponseBody_get_Detail_m685F0F780303CEDD3E219878ABBCDAB71D49DA16 (void);
// 0x00000098 System.String Unity.Services.Qos.Models.ErrorResponseBody::get_Instance()
extern void ErrorResponseBody_get_Instance_m0DBA78EB41CD0AE3A218F39D4D03DD9783F7D05B (void);
// 0x00000099 System.Collections.Generic.List`1<System.Object> Unity.Services.Qos.Models.ErrorResponseBody::get_Details()
extern void ErrorResponseBody_get_Details_m23800634EE62A708C6F4B020B259766ADE9EA048 (void);
// 0x0000009A System.Void Unity.Services.Qos.Models.KeyValuePair::.ctor(System.String,System.String)
extern void KeyValuePair__ctor_mA8C81C1804EAA717255BA0B43B9030EC37B77448 (void);
// 0x0000009B System.String Unity.Services.Qos.Models.KeyValuePair::get_Key()
extern void KeyValuePair_get_Key_mF923D1589D475573BBC8FA490E960667AB1F756A (void);
// 0x0000009C System.String Unity.Services.Qos.Models.KeyValuePair::get_Value()
extern void KeyValuePair_get_Value_m50CA3CC82B0C2A2FA87F600205DD564024461CFE (void);
// 0x0000009D System.Type Unity.Services.Qos.Models.IOneOf::get_Type()
// 0x0000009E System.Object Unity.Services.Qos.Models.IOneOf::get_Value()
// 0x0000009F System.Void Unity.Services.Qos.Models.QosServer::.ctor(System.Collections.Generic.List`1<System.String>,System.String,System.Collections.Generic.List`1<System.String>)
extern void QosServer__ctor_mC629272624FD8B341620B53A52EF8C535E3535DC (void);
// 0x000000A0 System.Collections.Generic.List`1<System.String> Unity.Services.Qos.Models.QosServer::get_Endpoints()
extern void QosServer_get_Endpoints_mD617719F90188011B22A4FA9C155299FE9CDB703 (void);
// 0x000000A1 System.String Unity.Services.Qos.Models.QosServer::get_Region()
extern void QosServer_get_Region_mD34E1FEF6D71BA6B31A8E1670F01E516FF173141 (void);
// 0x000000A2 System.Collections.Generic.List`1<System.String> Unity.Services.Qos.Models.QosServer::get_Services()
extern void QosServer_get_Services_mAF031EDF95DF0D7C94917C41468795BDA553797B (void);
// 0x000000A3 System.Void Unity.Services.Qos.Models.QosServersList::.ctor(System.Collections.Generic.List`1<Unity.Services.Qos.Models.QosServer>)
extern void QosServersList__ctor_m2BF251BB54BEA0FA96F0EFD72EF6748DF6703479 (void);
// 0x000000A4 System.Collections.Generic.List`1<Unity.Services.Qos.Models.QosServer> Unity.Services.Qos.Models.QosServersList::get_Servers()
extern void QosServersList_get_Servers_m50BE80B5A162F0A4CEBED1C13999395CDC32E794 (void);
// 0x000000A5 System.Void Unity.Services.Qos.Models.QosServersResponseBody::.ctor(Unity.Services.Qos.Models.QosServersList)
extern void QosServersResponseBody__ctor_m2298B57029DEEBB06027B40E1193737DEE9801DD (void);
// 0x000000A6 Unity.Services.Qos.Models.QosServersList Unity.Services.Qos.Models.QosServersResponseBody::get_Data()
extern void QosServersResponseBody_get_Data_mE5664C85DA17FC35E4BB7FE93E65FDD046FE0E71 (void);
// 0x000000A7 System.Void Unity.Services.Qos.Http.BaseApiClient::.ctor(Unity.Services.Qos.Http.IHttpClient)
extern void BaseApiClient__ctor_m99B4C250174C7CCEF6640C19E082E9FEF4DF0F99 (void);
// 0x000000A8 System.Void Unity.Services.Qos.Http.DeserializationException::.ctor()
extern void DeserializationException__ctor_m9CC49208201EB2245A19CB9111E48E0C9AAD9AFC (void);
// 0x000000A9 System.Void Unity.Services.Qos.Http.DeserializationException::.ctor(System.String)
extern void DeserializationException__ctor_m969A27E83C9B37A47D137A5E4B7B9F7BBA4F5330 (void);
// 0x000000AA System.Void Unity.Services.Qos.Http.DeserializationException::.ctor(System.String,System.Exception)
extern void DeserializationException__ctor_mDBCD4FBA44EFF08B6C206A15B9F14C34BEC18F98 (void);
// 0x000000AB System.Void Unity.Services.Qos.Http.DeserializationSettings::.ctor()
extern void DeserializationSettings__ctor_m82FDFFCE4B4F48D1131AAD21C5B87C5E5DFD2F0D (void);
// 0x000000AC System.Void Unity.Services.Qos.Http.HttpClient::.ctor()
extern void HttpClient__ctor_m280356AE0B5577C3BF6544E6673E70C13458016A (void);
// 0x000000AD System.Threading.Tasks.Task`1<Unity.Services.Qos.Http.HttpClientResponse> Unity.Services.Qos.Http.HttpClient::MakeRequestAsync(System.String,System.String,System.Byte[],System.Collections.Generic.Dictionary`2<System.String,System.String>,System.Int32)
extern void HttpClient_MakeRequestAsync_m59AEFEF16C8062494F149D5D8EF24EEF5951C138 (void);
// 0x000000AE System.Threading.Tasks.Task`1<Unity.Services.Qos.Http.HttpClientResponse> Unity.Services.Qos.Http.HttpClient::MakeRequestAsync(System.String,System.String,System.Collections.Generic.List`1<UnityEngine.Networking.IMultipartFormSection>,System.Collections.Generic.Dictionary`2<System.String,System.String>,System.Int32,System.String)
extern void HttpClient_MakeRequestAsync_m50DC00F79426CC78DC03EA6E492D2224A2D27F30 (void);
// 0x000000AF System.Collections.IEnumerator Unity.Services.Qos.Http.HttpClient::ProcessRequest(System.String,System.String,System.Collections.Generic.IDictionary`2<System.String,System.String>,System.Byte[],System.Int32,System.Action`1<Unity.Services.Qos.Http.HttpClientResponse>)
extern void HttpClient_ProcessRequest_mE537ED13A9B5BCC060DAA8B33F2038677A899098 (void);
// 0x000000B0 UnityEngine.Networking.UnityWebRequest Unity.Services.Qos.Http.HttpClient::CreateWebRequest(System.String,System.String,System.Byte[],System.Collections.Generic.IDictionary`2<System.String,System.String>,System.Int32)
extern void HttpClient_CreateWebRequest_m32466AF8B4E31091CB738CAFA88C8F42FCDD686E (void);
// 0x000000B1 System.Threading.Tasks.Task`1<Unity.Services.Qos.Http.HttpClientResponse> Unity.Services.Qos.Http.HttpClient::CreateWebRequestAsync(System.String,System.String,System.Byte[],System.Collections.Generic.IDictionary`2<System.String,System.String>,System.Int32)
extern void HttpClient_CreateWebRequestAsync_mE29C3A2AD414500FB9B360CEA834FA802957E413 (void);
// 0x000000B2 System.Threading.Tasks.Task`1<Unity.Services.Qos.Http.HttpClientResponse> Unity.Services.Qos.Http.HttpClient::CreateWebRequestAsync(System.String,System.String,System.Collections.Generic.List`1<UnityEngine.Networking.IMultipartFormSection>,System.Collections.Generic.IDictionary`2<System.String,System.String>,System.Int32,System.String)
extern void HttpClient_CreateWebRequestAsync_m7A2A754949C98CCAECD0D937602197F226EE3586 (void);
// 0x000000B3 UnityEngine.Networking.UnityWebRequest Unity.Services.Qos.Http.HttpClient::SetupMultipartRequest(UnityEngine.Networking.UnityWebRequest,System.Collections.Generic.List`1<UnityEngine.Networking.IMultipartFormSection>,System.Byte[])
extern void HttpClient_SetupMultipartRequest_m942E9984424A47309D6CEC78FA2B679EEB0D3D90 (void);
// 0x000000B4 Unity.Services.Qos.Http.HttpClientResponse Unity.Services.Qos.Http.HttpClient::CreateHttpClientResponse(UnityEngine.Networking.UnityWebRequestAsyncOperation)
extern void HttpClient_CreateHttpClientResponse_m1CF6C4DE6D8B11053BD0569304901C5A72624B10 (void);
// 0x000000B5 System.Void Unity.Services.Qos.Http.HttpClient::.cctor()
extern void HttpClient__cctor_m81F42CB474E726969980761A454A6DE4F6006201 (void);
// 0x000000B6 System.Boolean Unity.Services.Qos.Http.HttpClient::<ProcessRequest>g__ShouldRetry|5_1(UnityEngine.Networking.UnityWebRequestAsyncOperation)
extern void HttpClient_U3CProcessRequestU3Eg__ShouldRetryU7C5_1_m3FF51D7C80DF0578391FD07EC6365CCD2B12E576 (void);
// 0x000000B7 System.Void Unity.Services.Qos.Http.HttpClient/<MakeRequestAsync>d__3::MoveNext()
extern void U3CMakeRequestAsyncU3Ed__3_MoveNext_m46149872A41451C0459335243024746705EC7CB5 (void);
// 0x000000B8 System.Void Unity.Services.Qos.Http.HttpClient/<MakeRequestAsync>d__3::SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine)
extern void U3CMakeRequestAsyncU3Ed__3_SetStateMachine_m76B718098FE85BF2B623BBE1C88AB0EE1484612D (void);
// 0x000000B9 System.Void Unity.Services.Qos.Http.HttpClient/<MakeRequestAsync>d__4::MoveNext()
extern void U3CMakeRequestAsyncU3Ed__4_MoveNext_mC01E6A0864677EBA487FA6E848EBAD9ADD8D2AF4 (void);
// 0x000000BA System.Void Unity.Services.Qos.Http.HttpClient/<MakeRequestAsync>d__4::SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine)
extern void U3CMakeRequestAsyncU3Ed__4_SetStateMachine_mE23586ACBF3E089A1599DE1D155480B489C9E913 (void);
// 0x000000BB System.Void Unity.Services.Qos.Http.HttpClient/<>c__DisplayClass5_0::.ctor()
extern void U3CU3Ec__DisplayClass5_0__ctor_m40692977886B6F2A77F04D996EF600E964D2DD63 (void);
// 0x000000BC UnityEngine.Networking.UnityWebRequestAsyncOperation Unity.Services.Qos.Http.HttpClient/<>c__DisplayClass5_0::<ProcessRequest>g__SetupRequest|0(System.Int32)
extern void U3CU3Ec__DisplayClass5_0_U3CProcessRequestU3Eg__SetupRequestU7C0_m2123AF8E03516D225FC5F04D5AFC8A694C86261B (void);
// 0x000000BD System.Void Unity.Services.Qos.Http.HttpClient/<>c__DisplayClass5_0::<ProcessRequest>g__AsyncOpCompleted|2(UnityEngine.Networking.UnityWebRequestAsyncOperation)
extern void U3CU3Ec__DisplayClass5_0_U3CProcessRequestU3Eg__AsyncOpCompletedU7C2_m47623757DD2D730990359F8BBB3E3DFB6684F67B (void);
// 0x000000BE System.Void Unity.Services.Qos.Http.HttpClient/<ProcessRequest>d__5::.ctor(System.Int32)
extern void U3CProcessRequestU3Ed__5__ctor_m159E20D0FE626ECBA99229CCAD562725C077E107 (void);
// 0x000000BF System.Void Unity.Services.Qos.Http.HttpClient/<ProcessRequest>d__5::System.IDisposable.Dispose()
extern void U3CProcessRequestU3Ed__5_System_IDisposable_Dispose_mE4B58F85188635F8756520E72896A58F228A04AB (void);
// 0x000000C0 System.Boolean Unity.Services.Qos.Http.HttpClient/<ProcessRequest>d__5::MoveNext()
extern void U3CProcessRequestU3Ed__5_MoveNext_mC6C2BAC5A58D2C4218D0637E065DDA70A658D07F (void);
// 0x000000C1 System.Object Unity.Services.Qos.Http.HttpClient/<ProcessRequest>d__5::System.Collections.Generic.IEnumerator<System.Object>.get_Current()
extern void U3CProcessRequestU3Ed__5_System_Collections_Generic_IEnumeratorU3CSystem_ObjectU3E_get_Current_m63DFD04C43BA81FAFCECD61D026A1C9CDD11AC86 (void);
// 0x000000C2 System.Void Unity.Services.Qos.Http.HttpClient/<ProcessRequest>d__5::System.Collections.IEnumerator.Reset()
extern void U3CProcessRequestU3Ed__5_System_Collections_IEnumerator_Reset_m48F7D010406A28FFE172D01326774E590C4994CD (void);
// 0x000000C3 System.Object Unity.Services.Qos.Http.HttpClient/<ProcessRequest>d__5::System.Collections.IEnumerator.get_Current()
extern void U3CProcessRequestU3Ed__5_System_Collections_IEnumerator_get_Current_m0F3CD482F89F40563CBB07A4B5DACAFDA668A387 (void);
// 0x000000C4 System.Void Unity.Services.Qos.Http.HttpClient/<>c__DisplayClass7_0::.ctor()
extern void U3CU3Ec__DisplayClass7_0__ctor_mB57252D69145549A05B7AC369DB36B5C22A51D3A (void);
// 0x000000C5 System.Threading.Tasks.Task`1<Unity.Services.Qos.Http.HttpClientResponse> Unity.Services.Qos.Http.HttpClient/<>c__DisplayClass7_0::<CreateWebRequestAsync>b__0()
extern void U3CU3Ec__DisplayClass7_0_U3CCreateWebRequestAsyncU3Eb__0_mE0836D216B45384B3487278240EB02DD51FD5BDD (void);
// 0x000000C6 System.Void Unity.Services.Qos.Http.HttpClient/<>c__DisplayClass7_0/<<CreateWebRequestAsync>b__0>d::MoveNext()
extern void U3CU3CCreateWebRequestAsyncU3Eb__0U3Ed_MoveNext_mD060FEB8287E76472F4F91733E874C4702E787B5 (void);
// 0x000000C7 System.Void Unity.Services.Qos.Http.HttpClient/<>c__DisplayClass7_0/<<CreateWebRequestAsync>b__0>d::SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine)
extern void U3CU3CCreateWebRequestAsyncU3Eb__0U3Ed_SetStateMachine_m4ABB7FBDA2D9CB66079338761CD944DE4C9A603F (void);
// 0x000000C8 System.Void Unity.Services.Qos.Http.HttpClient/<CreateWebRequestAsync>d__7::MoveNext()
extern void U3CCreateWebRequestAsyncU3Ed__7_MoveNext_mF01B9C370C7CFE15812010FA434DC2D5689B9586 (void);
// 0x000000C9 System.Void Unity.Services.Qos.Http.HttpClient/<CreateWebRequestAsync>d__7::SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine)
extern void U3CCreateWebRequestAsyncU3Ed__7_SetStateMachine_mB2F0C29A29F66A7401BEF44EB6694437FD707ED1 (void);
// 0x000000CA System.Void Unity.Services.Qos.Http.HttpClient/<>c__DisplayClass8_0::.ctor()
extern void U3CU3Ec__DisplayClass8_0__ctor_m9045A2D19FC765ACFB746A91F9C83040A6496DA5 (void);
// 0x000000CB System.Threading.Tasks.Task`1<Unity.Services.Qos.Http.HttpClientResponse> Unity.Services.Qos.Http.HttpClient/<>c__DisplayClass8_0::<CreateWebRequestAsync>b__0()
extern void U3CU3Ec__DisplayClass8_0_U3CCreateWebRequestAsyncU3Eb__0_m219E2AF1C9B724DD612275B1FA10C78F144E65EF (void);
// 0x000000CC System.Void Unity.Services.Qos.Http.HttpClient/<>c__DisplayClass8_0/<<CreateWebRequestAsync>b__0>d::MoveNext()
extern void U3CU3CCreateWebRequestAsyncU3Eb__0U3Ed_MoveNext_m8934FB8022AE7CF1AB11F06E3CB42A38CE255D13 (void);
// 0x000000CD System.Void Unity.Services.Qos.Http.HttpClient/<>c__DisplayClass8_0/<<CreateWebRequestAsync>b__0>d::SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine)
extern void U3CU3CCreateWebRequestAsyncU3Eb__0U3Ed_SetStateMachine_m0FF0BD3CB916FC6CC57BA1813B2F100A7A7FFE85 (void);
// 0x000000CE System.Void Unity.Services.Qos.Http.HttpClient/<CreateWebRequestAsync>d__8::MoveNext()
extern void U3CCreateWebRequestAsyncU3Ed__8_MoveNext_mE8324F913699569B8A7885F369C06A1BC51AFE4E (void);
// 0x000000CF System.Void Unity.Services.Qos.Http.HttpClient/<CreateWebRequestAsync>d__8::SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine)
extern void U3CCreateWebRequestAsyncU3Ed__8_SetStateMachine_mB1FD0ECA05BACCB6EDBB4A807DD74C21E6EB016A (void);
// 0x000000D0 System.Void Unity.Services.Qos.Http.HttpClientResponse::.ctor(System.Collections.Generic.Dictionary`2<System.String,System.String>,System.Int64,System.Boolean,System.Boolean,System.Byte[],System.String)
extern void HttpClientResponse__ctor_mD1FC246D901ACC12807D2B5C1DB39B038622A4E0 (void);
// 0x000000D1 System.Collections.Generic.Dictionary`2<System.String,System.String> Unity.Services.Qos.Http.HttpClientResponse::get_Headers()
extern void HttpClientResponse_get_Headers_mF870416900DD323B636429C079B0BCAE39AEA2AA (void);
// 0x000000D2 System.Int64 Unity.Services.Qos.Http.HttpClientResponse::get_StatusCode()
extern void HttpClientResponse_get_StatusCode_mC8A80B746A2F6E82F65517C3AB4C479784B55998 (void);
// 0x000000D3 System.Boolean Unity.Services.Qos.Http.HttpClientResponse::get_IsHttpError()
extern void HttpClientResponse_get_IsHttpError_m6DCA23589DA544E4E462A755FA5E781206995A3B (void);
// 0x000000D4 System.Boolean Unity.Services.Qos.Http.HttpClientResponse::get_IsNetworkError()
extern void HttpClientResponse_get_IsNetworkError_m3C3F6623CBFEFED95570B0EC1677CA7E31C9DAE0 (void);
// 0x000000D5 System.Byte[] Unity.Services.Qos.Http.HttpClientResponse::get_Data()
extern void HttpClientResponse_get_Data_mB0A3BB94BF7CE548D1464659194CA4E9557E09DE (void);
// 0x000000D6 System.String Unity.Services.Qos.Http.HttpClientResponse::get_ErrorMessage()
extern void HttpClientResponse_get_ErrorMessage_mC9BA60D07DC70CF2BAA92C4C256BDF5CB74F326B (void);
// 0x000000D7 System.Void Unity.Services.Qos.Http.HttpException::.ctor()
extern void HttpException__ctor_mCC244F8536EC09FF5682C779CE590A4CCC92083A (void);
// 0x000000D8 System.Void Unity.Services.Qos.Http.HttpException::.ctor(System.String)
extern void HttpException__ctor_mB86916F592FA13816761064DA3D9F23C54ACB6F3 (void);
// 0x000000D9 System.Void Unity.Services.Qos.Http.HttpException::.ctor(System.String,System.Exception)
extern void HttpException__ctor_m776EE74F052257E8AE9ABCF9C16CBF5774673578 (void);
// 0x000000DA System.Void Unity.Services.Qos.Http.HttpException::.ctor(Unity.Services.Qos.Http.HttpClientResponse)
extern void HttpException__ctor_m4927A27335A8E59162013A85EF8972D88AA0754C (void);
// 0x000000DB System.Void Unity.Services.Qos.Http.HttpException`1::.ctor()
// 0x000000DC System.Void Unity.Services.Qos.Http.HttpException`1::.ctor(System.String)
// 0x000000DD System.Void Unity.Services.Qos.Http.HttpException`1::.ctor(System.String,System.Exception)
// 0x000000DE System.Void Unity.Services.Qos.Http.HttpException`1::.ctor(Unity.Services.Qos.Http.HttpClientResponse,T)
// 0x000000DF System.Threading.Tasks.Task`1<Unity.Services.Qos.Http.HttpClientResponse> Unity.Services.Qos.Http.IHttpClient::MakeRequestAsync(System.String,System.String,System.Byte[],System.Collections.Generic.Dictionary`2<System.String,System.String>,System.Int32)
// 0x000000E0 System.Threading.Tasks.Task`1<Unity.Services.Qos.Http.HttpClientResponse> Unity.Services.Qos.Http.IHttpClient::MakeRequestAsync(System.String,System.String,System.Collections.Generic.List`1<UnityEngine.Networking.IMultipartFormSection>,System.Collections.Generic.Dictionary`2<System.String,System.String>,System.Int32,System.String)
// 0x000000E1 System.Void Unity.Services.Qos.Http.JsonHelpers::RegisterTypesForAOT()
extern void JsonHelpers_RegisterTypesForAOT_m974460CCF262A51C0C6A1A6DC8A5DD8C0E65BDB5 (void);
// 0x000000E2 System.Boolean Unity.Services.Qos.Http.JsonHelpers::TryParseJson(System.String,T&)
// 0x000000E3 System.Void Unity.Services.Qos.Http.JsonHelpers/<>c__DisplayClass1_0`1::.ctor()
// 0x000000E4 System.Void Unity.Services.Qos.Http.JsonHelpers/<>c__DisplayClass1_0`1::<TryParseJson>b__0(System.Object,Newtonsoft.Json.Serialization.ErrorEventArgs)
// 0x000000E5 System.Void Unity.Services.Qos.Http.JsonObject::.ctor(System.Object)
extern void JsonObject__ctor_m400BFEB71293B1BAE2A146047B091BC8CB26C419 (void);
// 0x000000E6 System.String Unity.Services.Qos.Http.JsonObject::GetAsString()
extern void JsonObject_GetAsString_m8CAC654D2EBE92E309029903CE9BC57DBB9BE8F3 (void);
// 0x000000E7 T Unity.Services.Qos.Http.JsonObject::GetAs(Unity.Services.Qos.Http.DeserializationSettings)
// 0x000000E8 System.Collections.Generic.List`1<System.String> Unity.Services.Qos.Http.JsonObject::ValidateObject(T,System.Collections.Generic.List`1<System.String>)
// 0x000000E9 System.Void Unity.Services.Qos.Http.JsonObject::ValidatePropertyInfos(T,System.Collections.Generic.List`1<System.String>)
// 0x000000EA System.Void Unity.Services.Qos.Http.JsonObject::ValidateFieldInfos(T,System.Collections.Generic.List`1<System.String>)
// 0x000000EB System.Void Unity.Services.Qos.Http.JsonObject::ValidateValue(System.Object,System.String,System.String,System.String,System.Collections.Generic.List`1<System.String>)
extern void JsonObject_ValidateValue_m3D015321ABD5AE6AED7B475B6D3C34F0BCE63D80 (void);
// 0x000000EC System.Void Unity.Services.Qos.Http.JsonObjectConverter::WriteJson(Newtonsoft.Json.JsonWriter,System.Object,Newtonsoft.Json.JsonSerializer)
extern void JsonObjectConverter_WriteJson_m4443914EDF2F50BD076694458FA416DD5E820385 (void);
// 0x000000ED System.Object Unity.Services.Qos.Http.JsonObjectConverter::ReadJson(Newtonsoft.Json.JsonReader,System.Type,System.Object,Newtonsoft.Json.JsonSerializer)
extern void JsonObjectConverter_ReadJson_m15F9F7E014B8BDC97D20CBAC668D406681702479 (void);
// 0x000000EE System.Boolean Unity.Services.Qos.Http.JsonObjectConverter::CanConvert(System.Type)
extern void JsonObjectConverter_CanConvert_m671986316D3626EE88F9D7F3FE6FE85F217DCDD9 (void);
// 0x000000EF System.Void Unity.Services.Qos.Http.JsonObjectConverter::.ctor()
extern void JsonObjectConverter__ctor_m3590310A54D23B98456D5A20348876A06E517477 (void);
// 0x000000F0 System.Void Unity.Services.Qos.Http.ResponseDeserializationException::.ctor()
extern void ResponseDeserializationException__ctor_mBB646EAD382A7D28AC499D61C855E3D206D23ED6 (void);
// 0x000000F1 System.Void Unity.Services.Qos.Http.ResponseDeserializationException::.ctor(System.String)
extern void ResponseDeserializationException__ctor_m704DEEF38F300A4921D3EDD6C564761DFE661C53 (void);
// 0x000000F2 System.Void Unity.Services.Qos.Http.ResponseDeserializationException::.ctor(System.Exception,System.String)
extern void ResponseDeserializationException__ctor_mE6E1AB43F6B88301B5245AB34E1098448BC0554A (void);
// 0x000000F3 System.Void Unity.Services.Qos.Http.ResponseDeserializationException::.ctor(Unity.Services.Qos.Http.HttpClientResponse)
extern void ResponseDeserializationException__ctor_m1DE598AF93250D2B52554D9769AC6800E23B124D (void);
// 0x000000F4 System.Void Unity.Services.Qos.Http.ResponseDeserializationException::.ctor(Unity.Services.Qos.Http.HttpClientResponse,System.String)
extern void ResponseDeserializationException__ctor_m6B1A5AAC07DBF1DE306A000ECF99CE9648615C1B (void);
// 0x000000F5 System.Void Unity.Services.Qos.Http.ResponseDeserializationException::.ctor(Unity.Services.Qos.Http.HttpClientResponse,System.Exception,System.String)
extern void ResponseDeserializationException__ctor_mA4DEFE9DC1A572D228890B5512CB493D1282324B (void);
// 0x000000F6 T Unity.Services.Qos.Http.ResponseHandler::TryDeserializeResponse(Unity.Services.Qos.Http.HttpClientResponse)
// 0x000000F7 System.Object Unity.Services.Qos.Http.ResponseHandler::TryDeserializeResponse(Unity.Services.Qos.Http.HttpClientResponse,System.Type)
extern void ResponseHandler_TryDeserializeResponse_m8B10BE8FAD4A565918C93EBDAF0369E4486AE8D4 (void);
// 0x000000F8 System.String Unity.Services.Qos.Http.ResponseHandler::GetDeserializedJson(System.Byte[])
extern void ResponseHandler_GetDeserializedJson_m677949D173DF6A65ADDAB1AD7FD2B972F3598C08 (void);
// 0x000000F9 System.Void Unity.Services.Qos.Http.ResponseHandler::HandleAsyncResponse(Unity.Services.Qos.Http.HttpClientResponse,System.Collections.Generic.Dictionary`2<System.String,System.Type>)
extern void ResponseHandler_HandleAsyncResponse_mF85227D729EBBB63B3F269D558F28D7B3477CB31 (void);
// 0x000000FA Unity.Services.Qos.Http.HttpException Unity.Services.Qos.Http.ResponseHandler::CreateOneOfException(Unity.Services.Qos.Http.HttpClientResponse,System.Type)
extern void ResponseHandler_CreateOneOfException_m997402BEBF88FA749E79E2FED79A3FEAFCE53AC7 (void);
// 0x000000FB Unity.Services.Qos.Http.HttpException Unity.Services.Qos.Http.ResponseHandler::CreateHttpException(Unity.Services.Qos.Http.HttpClientResponse,System.Type)
extern void ResponseHandler_CreateHttpException_m48860939F71126F5AC48218C48CA033069D606F8 (void);
// 0x000000FC T Unity.Services.Qos.Http.ResponseHandler::HandleAsyncResponse(Unity.Services.Qos.Http.HttpClientResponse,System.Collections.Generic.Dictionary`2<System.String,System.Type>)
// 0x000000FD System.Runtime.CompilerServices.TaskAwaiter`1<Unity.Services.Qos.Http.HttpClientResponse> Unity.Services.Qos.Http.UnityWebRequestHelpers::GetAwaiter(UnityEngine.Networking.UnityWebRequestAsyncOperation)
extern void UnityWebRequestHelpers_GetAwaiter_mFE3CEA3273241928556A3EE6EB2D699C01EF52F7 (void);
// 0x000000FE Unity.Services.Qos.Http.HttpClientResponse Unity.Services.Qos.Http.UnityWebRequestHelpers::CreateHttpClientResponse(UnityEngine.Networking.UnityWebRequestAsyncOperation)
extern void UnityWebRequestHelpers_CreateHttpClientResponse_m83A8F9B40812C3017F91315CC83DEA0BE3D74123 (void);
// 0x000000FF System.Void Unity.Services.Qos.Http.UnityWebRequestHelpers/<>c__DisplayClass0_0::.ctor()
extern void U3CU3Ec__DisplayClass0_0__ctor_m1A61824C8360274E7FD6DBB60E9D9060BB60DD89 (void);
// 0x00000100 System.Void Unity.Services.Qos.Http.UnityWebRequestHelpers/<>c__DisplayClass0_0::<GetAwaiter>b__0(UnityEngine.AsyncOperation)
extern void U3CU3Ec__DisplayClass0_0_U3CGetAwaiterU3Eb__0_m57EB7F27B683B8FEE80D26546C11F61A1718DF66 (void);
// 0x00000101 Unity.Services.Qos.Helpers.AsyncOpRetry`1<T> Unity.Services.Qos.Helpers.AsyncOpRetry::FromCreateAsync(System.Func`2<System.Int32,T>)
// 0x00000102 System.UInt32 Unity.Services.Qos.Helpers.AsyncOpRetry`1::get_MaxRetries()
// 0x00000103 System.Void Unity.Services.Qos.Helpers.AsyncOpRetry`1::set_MaxRetries(System.UInt32)
// 0x00000104 System.Single Unity.Services.Qos.Helpers.AsyncOpRetry`1::get_JitterMagnitude()
// 0x00000105 System.Void Unity.Services.Qos.Helpers.AsyncOpRetry`1::set_JitterMagnitude(System.Single)
// 0x00000106 System.Single Unity.Services.Qos.Helpers.AsyncOpRetry`1::get_DelayScale()
// 0x00000107 System.Void Unity.Services.Qos.Helpers.AsyncOpRetry`1::set_DelayScale(System.Single)
// 0x00000108 System.Single Unity.Services.Qos.Helpers.AsyncOpRetry`1::get_MaxDelayTime()
// 0x00000109 System.Void Unity.Services.Qos.Helpers.AsyncOpRetry`1::set_MaxDelayTime(System.Single)
// 0x0000010A System.Func`2<System.Int32,T> Unity.Services.Qos.Helpers.AsyncOpRetry`1::get_CreateOperation()
// 0x0000010B System.Void Unity.Services.Qos.Helpers.AsyncOpRetry`1::set_CreateOperation(System.Func`2<System.Int32,T>)
// 0x0000010C System.Func`2<T,System.Boolean> Unity.Services.Qos.Helpers.AsyncOpRetry`1::get_RetryCondition()
// 0x0000010D System.Void Unity.Services.Qos.Helpers.AsyncOpRetry`1::set_RetryCondition(System.Func`2<T,System.Boolean>)
// 0x0000010E System.Action`1<T> Unity.Services.Qos.Helpers.AsyncOpRetry`1::get_OnComplete()
// 0x0000010F System.Void Unity.Services.Qos.Helpers.AsyncOpRetry`1::set_OnComplete(System.Action`1<T>)
// 0x00000110 System.Void Unity.Services.Qos.Helpers.AsyncOpRetry`1::.ctor(System.Func`2<System.Int32,T>)
// 0x00000111 System.Single Unity.Services.Qos.Helpers.AsyncOpRetry`1::AddJitter(System.Single,System.Single)
// 0x00000112 System.Single Unity.Services.Qos.Helpers.AsyncOpRetry`1::Pow2(System.Single,System.Single)
// 0x00000113 System.Single Unity.Services.Qos.Helpers.AsyncOpRetry`1::CalculateDelay(System.Int32,System.Single,System.Single,System.Single)
// 0x00000114 Unity.Services.Qos.Helpers.AsyncOpRetry`1<T> Unity.Services.Qos.Helpers.AsyncOpRetry`1::FromCreateAsync(System.Func`2<System.Int32,T>)
// 0x00000115 Unity.Services.Qos.Helpers.AsyncOpRetry`1<T> Unity.Services.Qos.Helpers.AsyncOpRetry`1::WithRetryCondition(System.Func`2<T,System.Boolean>)
// 0x00000116 Unity.Services.Qos.Helpers.AsyncOpRetry`1<T> Unity.Services.Qos.Helpers.AsyncOpRetry`1::WhenComplete(System.Action`1<T>)
// 0x00000117 System.Collections.IEnumerator Unity.Services.Qos.Helpers.AsyncOpRetry`1::Run()
// 0x00000118 System.Void Unity.Services.Qos.Helpers.AsyncOpRetry`1/<Run>d__35::.ctor(System.Int32)
// 0x00000119 System.Void Unity.Services.Qos.Helpers.AsyncOpRetry`1/<Run>d__35::System.IDisposable.Dispose()
// 0x0000011A System.Boolean Unity.Services.Qos.Helpers.AsyncOpRetry`1/<Run>d__35::MoveNext()
// 0x0000011B System.Object Unity.Services.Qos.Helpers.AsyncOpRetry`1/<Run>d__35::System.Collections.Generic.IEnumerator<System.Object>.get_Current()
// 0x0000011C System.Void Unity.Services.Qos.Helpers.AsyncOpRetry`1/<Run>d__35::System.Collections.IEnumerator.Reset()
// 0x0000011D System.Object Unity.Services.Qos.Helpers.AsyncOpRetry`1/<Run>d__35::System.Collections.IEnumerator.get_Current()
// 0x0000011E System.Byte[] Unity.Services.Qos.QosDiscovery.JsonSerialization::Serialize(T)
// 0x0000011F System.String Unity.Services.Qos.QosDiscovery.JsonSerialization::SerializeToString(T)
// 0x00000120 System.Collections.Generic.List`1<System.String> Unity.Services.Qos.QosDiscovery.QosDiscoveryApiBaseRequest::AddParamsToQueryParams(System.Collections.Generic.List`1<System.String>,System.String,System.String)
extern void QosDiscoveryApiBaseRequest_AddParamsToQueryParams_m37EBFF6DDCA6400B0C77F2CABAB42FDFA86B5A97 (void);
// 0x00000121 System.Collections.Generic.List`1<System.String> Unity.Services.Qos.QosDiscovery.QosDiscoveryApiBaseRequest::AddParamsToQueryParams(System.Collections.Generic.List`1<System.String>,System.String,System.Collections.Generic.List`1<System.String>,System.String,System.Boolean)
extern void QosDiscoveryApiBaseRequest_AddParamsToQueryParams_m56323A5B31B725034B0CA9AE992F7FB2946AE2FD (void);
// 0x00000122 System.Collections.Generic.List`1<System.String> Unity.Services.Qos.QosDiscovery.QosDiscoveryApiBaseRequest::AddParamsToQueryParams(System.Collections.Generic.List`1<System.String>,System.String,T)
// 0x00000123 System.String Unity.Services.Qos.QosDiscovery.QosDiscoveryApiBaseRequest::GetPathParamString(System.Collections.Generic.List`1<System.String>)
extern void QosDiscoveryApiBaseRequest_GetPathParamString_m175BA1D606F251D4F8318DF77732D96F9BCFDC7D (void);
// 0x00000124 System.Byte[] Unity.Services.Qos.QosDiscovery.QosDiscoveryApiBaseRequest::ConstructBody(System.IO.Stream)
extern void QosDiscoveryApiBaseRequest_ConstructBody_m7F185A28CF2CCE8BC8E0CBCB25D6AEFEDE0126D0 (void);
// 0x00000125 System.Byte[] Unity.Services.Qos.QosDiscovery.QosDiscoveryApiBaseRequest::ConstructBody(System.String)
extern void QosDiscoveryApiBaseRequest_ConstructBody_mDD86CD521B7A69DA0C57CFFCF5F0AC6CEDB09817 (void);
// 0x00000126 System.Byte[] Unity.Services.Qos.QosDiscovery.QosDiscoveryApiBaseRequest::ConstructBody(System.Object)
extern void QosDiscoveryApiBaseRequest_ConstructBody_m916EE630F6706AB71EFDFA875D84FD2684CE2C77 (void);
// 0x00000127 System.String Unity.Services.Qos.QosDiscovery.QosDiscoveryApiBaseRequest::GenerateAcceptHeader(System.String[])
extern void QosDiscoveryApiBaseRequest_GenerateAcceptHeader_m8393B909E55846FC30E166C3ED7E52CBFA9CFD2F (void);
// 0x00000128 System.String Unity.Services.Qos.QosDiscovery.QosDiscoveryApiBaseRequest::GenerateContentTypeHeader(System.String[])
extern void QosDiscoveryApiBaseRequest_GenerateContentTypeHeader_m8578BC7C80BEA3C4919B898356FC5A205B59DCE7 (void);
// 0x00000129 UnityEngine.Networking.IMultipartFormSection Unity.Services.Qos.QosDiscovery.QosDiscoveryApiBaseRequest::GenerateMultipartFormFileSection(System.String,System.IO.FileStream,System.String)
extern void QosDiscoveryApiBaseRequest_GenerateMultipartFormFileSection_mBBED2819CA892389B78A9DC3D78E90B2DDFBB1F8 (void);
// 0x0000012A UnityEngine.Networking.IMultipartFormSection Unity.Services.Qos.QosDiscovery.QosDiscoveryApiBaseRequest::GenerateMultipartFormFileSection(System.String,System.IO.Stream,System.String)
extern void QosDiscoveryApiBaseRequest_GenerateMultipartFormFileSection_mD0AF281D370DCAA50A68D266A19600CD6A6263BC (void);
// 0x0000012B System.String Unity.Services.Qos.QosDiscovery.QosDiscoveryApiBaseRequest::GetFileName(System.String)
extern void QosDiscoveryApiBaseRequest_GetFileName_m060FEF2B62E15D45760509984E2ABD20FF41FC2A (void);
// 0x0000012C System.Void Unity.Services.Qos.QosDiscovery.QosDiscoveryApiBaseRequest::.ctor()
extern void QosDiscoveryApiBaseRequest__ctor_m9122E977B93327ACE35337EA6E107C8EBC1C4A6A (void);
// 0x0000012D System.Void Unity.Services.Qos.QosDiscovery.QosDiscoveryApiBaseRequest::.cctor()
extern void QosDiscoveryApiBaseRequest__cctor_mB3CFEAA8095B2C5F15A1F5BAD07BB5EE915B4D99 (void);
// 0x0000012E System.Collections.Generic.List`1<System.String> Unity.Services.Qos.QosDiscovery.GetServersRequest::get_Region()
extern void GetServersRequest_get_Region_m49F11E7EA0533CD65FB26FBE102104D2338AC934 (void);
// 0x0000012F System.String Unity.Services.Qos.QosDiscovery.GetServersRequest::get_Service()
extern void GetServersRequest_get_Service_mD27F2D2FFD7924C9681C2116EFAFD03E77F59FEA (void);
// 0x00000130 System.Void Unity.Services.Qos.QosDiscovery.GetServersRequest::.ctor(System.Collections.Generic.List`1<System.String>,System.String)
extern void GetServersRequest__ctor_m454773232F666315509CCB10A2B5A029BD2CB70D (void);
// 0x00000131 System.String Unity.Services.Qos.QosDiscovery.GetServersRequest::ConstructUrl(System.String)
extern void GetServersRequest_ConstructUrl_m34E2AC5428BA279F3BF7001DB96FA0702B9D44C2 (void);
// 0x00000132 System.Byte[] Unity.Services.Qos.QosDiscovery.GetServersRequest::ConstructBody()
extern void GetServersRequest_ConstructBody_mCDAF08BF135ED626205192361C7757197C162166 (void);
// 0x00000133 System.Collections.Generic.Dictionary`2<System.String,System.String> Unity.Services.Qos.QosDiscovery.GetServersRequest::ConstructHeaders(Unity.Services.Authentication.Internal.IAccessToken,Unity.Services.Qos.Configuration)
extern void GetServersRequest_ConstructHeaders_m8CE30E99E33DDE136776B5830F2FCBF91C0B426A (void);
// 0x00000134 System.Void Unity.Services.Qos.QosDiscovery.GetServersRequest/<>c::.cctor()
extern void U3CU3Ec__cctor_m9AC91843F1D0511A7FFB24E03AFF31ED8BB08989 (void);
// 0x00000135 System.Void Unity.Services.Qos.QosDiscovery.GetServersRequest/<>c::.ctor()
extern void U3CU3Ec__ctor_m8002077DF6C1A68B5F9A799915DA6F1B9DC5B2FA (void);
// 0x00000136 System.String Unity.Services.Qos.QosDiscovery.GetServersRequest/<>c::<.ctor>b__7_0(System.String)
extern void U3CU3Ec_U3C_ctorU3Eb__7_0_mDF3BC19C96293831FC8ACA1C245D272CC33625A9 (void);
// 0x00000137 System.Threading.Tasks.Task`1<Unity.Services.Qos.Response`1<Unity.Services.Qos.Models.QosServersResponseBody>> Unity.Services.Qos.Apis.QosDiscovery.IQosDiscoveryApiClient::GetServersAsync(Unity.Services.Qos.QosDiscovery.GetServersRequest,Unity.Services.Qos.Configuration)
// 0x00000138 Unity.Services.Qos.Configuration Unity.Services.Qos.Apis.QosDiscovery.QosDiscoveryApiClient::get_Configuration()
extern void QosDiscoveryApiClient_get_Configuration_mA8F6B3F90810410D20D6420909967781E524A3FE (void);
// 0x00000139 System.Void Unity.Services.Qos.Apis.QosDiscovery.QosDiscoveryApiClient::set_Configuration(Unity.Services.Qos.Configuration)
extern void QosDiscoveryApiClient_set_Configuration_m98B22FA0BDAE4468C4E4B505D0AC6D4448CDFAA5 (void);
// 0x0000013A System.Void Unity.Services.Qos.Apis.QosDiscovery.QosDiscoveryApiClient::.ctor(Unity.Services.Qos.Http.IHttpClient,Unity.Services.Authentication.Internal.IAccessToken,Unity.Services.Qos.Configuration)
extern void QosDiscoveryApiClient__ctor_mAC0DD995EBE1EE7C9B7BF1D376B15451A3322D11 (void);
// 0x0000013B System.Threading.Tasks.Task`1<Unity.Services.Qos.Response`1<Unity.Services.Qos.Models.QosServersResponseBody>> Unity.Services.Qos.Apis.QosDiscovery.QosDiscoveryApiClient::GetServersAsync(Unity.Services.Qos.QosDiscovery.GetServersRequest,Unity.Services.Qos.Configuration)
extern void QosDiscoveryApiClient_GetServersAsync_m0ED9B245DB1571CD45BFF48C6A1968C44D2B545B (void);
// 0x0000013C System.Void Unity.Services.Qos.Apis.QosDiscovery.QosDiscoveryApiClient/<GetServersAsync>d__7::MoveNext()
extern void U3CGetServersAsyncU3Ed__7_MoveNext_m7D03C073B6C99374D7B4ED833585D6041991DB07 (void);
// 0x0000013D System.Void Unity.Services.Qos.Apis.QosDiscovery.QosDiscoveryApiClient/<GetServersAsync>d__7::SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine)
extern void U3CGetServersAsyncU3Ed__7_SetStateMachine_m1A5B420B9D92DA3196FDD52C3069A8DBDCFFD6D8 (void);
static Il2CppMethodPointer s_methodPointers[317] = 
{
	InternalQosResult_get_AverageLatencyMs_mC4DD63FA694AB15B32ECE7B95C391BF79250708B,
	InternalQosResult_get_PacketLoss_mAE93BDA8F14001F7D8B9B6B07D7BF5A6492F0E7A,
	InternalQosResult_AddAggregateLatency_mF4F807BAA262DEAE0E1B5A741C4D4F3B87AD363F,
	NULL,
	QosJob_get_QosResults_m99D294F773A497B20A7B607B3A5867AB1DD85CD6,
	QosJob__ctor_mC7757F9FE843176A16749879A99C777DD191D4BB,
	QosJob_Dispose_m023B42665979401AB7384B2BAB21B090BDDD21DF,
	QosJob_Execute_m66569856598381E3B9D9461F905CD7DD555819C6,
	QosJob_ProcessServers_m002469312F12627E8C8179045EF58AAEFC2767B6,
	QosJob_ProcessServer_m193D982E9EBF657DB785605C7ACFDE9FC1BFB121,
	QosJob_SendQosRequests_mE07A62D70704ABCBBFFD5C5B0A9E9D2075DC6A1E,
	QosJob_StoreServer_m10EC014D95822A081040752A7F218E8C3A511A09,
	QosJob_StoreResult_m4A593D9ECAFD18243069F8F1E01DD5362F5CCFC5,
	QosJob_RecvQosResponsesTimed_m05465557D4349CA3EB44FFFF643B1C32717301F6,
	QosJob_RecvQosResponses_mF7D285B242196414A29B2EB92995C41D5EF0E9CC,
	QosJob_EnableReceiveWait_m0411682DA5665E33783F6DE96212DEB4F369FC43,
	QosJob_LookupResult_mF78311CDB65FB597D261547BD872FBBB99D0D43E,
	QosJob_CreateAndBindSocket_mC2628791199CB10A3C0FCE263504FE8E9E552AD4,
	InternalQosServer__ctor_mDFFD17B79B32982F24BBF7814A2238B3A8DC1AA2,
	InternalQosServer_get_FirstIdx_mE1E26F2B9C43302D4D3A669677A32BB4D2CE0478,
	InternalQosServer_set_FirstIdx_mEE8FDA205B7B8B2DE6E8C6F1D0BFF888C1434A28,
	InternalQosServer_get_RequestIdentifier_m5BB7DCEB91EAD25DBD1733A74B42F4B490CCA2D5,
	InternalQosServer_set_RequestIdentifier_mAA9EF4C5C34AFB4EA335CBF999D2FBB46BD15E1F,
	InternalQosServer_get_Duplicate_mC9F0B30BEC3CEC496735613D59181989F13DEF2B,
	InternalQosServer_get_Address_mAF7047289348E24CA18A4202DA79D49C2E2298E4,
	NetworkEndPoint_get_Port_m251B69FEFF5FC2CF22A30DB9AA40348F340408CB,
	NetworkEndPoint_get_Family_mD35537952F0BCF8578F516DC06DC1D3B225F724F,
	NetworkEndPoint_get_Address_m1EB1D2DAF46FB52253E311B8423DF10FF8F03FBD,
	NetworkEndPoint_get_IsValid_m56A53E05D4C28A38048155E597009964FBAE2D94,
	NetworkEndPoint_TryParse_m290522A4D3F16227CB050DAAC84B0F7F9776E873,
	NetworkEndPoint_AddressAsString_mBF7B5153F6570D362AB7672C08CB24369B78D0E3,
	NetworkEndPoint_FromBaselibFamily_mF9E814790BF6646253228590A2C5938141A99CA9,
	NetworkEndPoint_ToBaselibFamily_m0F7E8A742E96033C58E29C69FBC20E3C5849DBB4,
	QosHelper_WouldBlock_mF00EFCE5355BC0FB84B9707F2B79BB6BD614A12D,
	QosHelper_ExpiredUtc_m78BBD5851F2AD5E38E8A7F369BC8B0A0F0241861,
	QosHelper_Since_m21E114D7D7BF2EAE40D75BEF8A64262D7E644EBC,
	QosRequest_get_Magic_m6F6463246F68BE8FCDACAC075DC10B8C08F23439,
	QosRequest_get_Version_mC41475C20C86D26270BBA934AA806FCD8A910238,
	QosRequest_get_FlowControl_m8B743B201483EC39C6F7E57579F36D841F276D03,
	QosRequest_get_Title_m9E68CBB84A55BD2B1E05F4943910C91A3ABE8236,
	QosRequest_set_Title_m835EE28AF9C6C4C8B30DE80EB01A61DFB27E52C2,
	QosRequest_get_Sequence_mB7BD623CF4FF0ADC6F36BBA4A5F83E5F4BB7892E,
	QosRequest_set_Sequence_m86A820A182DC7991C00DFB052A0130426633BEF7,
	QosRequest_get_Identifier_m5F1968614F882703F1BF258B7F920A4B43D8D062,
	QosRequest_set_Identifier_mBAA98E7B4ED9E4BC28CC5E29ADF69FE96C4987CF,
	QosRequest_get_Timestamp_mA705858832F72986788EAE43B411543CC563F5FD,
	QosRequest_set_Timestamp_m713A87D705A188F63BE58B901E51839C90214801,
	QosRequest_get_Length_mAA37DB5D869FE6DDAEEA9F5AD47E9109445A6853,
	QosRequest_Send_m29F081B6A23999A4F122B039D6843AB14B6B4C0E,
	QosRequest__ctor_m6DC94F804A906E394B07229DB0F6673A1D615395,
	QosResponse_get_Magic_m02CCA49C37D883E7F76ECCB379DF3FBC7A9212D2,
	QosResponse_get_Version_mA10B81D558000ED037D5C5B645E88FFD33A05747,
	QosResponse_get_FlowControl_mF09A6641107229409D421F7222B2B0BD2EDD677C,
	QosResponse_get_Sequence_m28659E53C16DABD437F1B3BFA4CDC600D86C6D5E,
	QosResponse_get_Identifier_mCEB0525F3D9FF3579526AAFA0D7B225D70BE927B,
	QosResponse_get_Timestamp_mB9E55DA94DCCBC222AA6C8B74DCE71BD40FF5169,
	QosResponse_get_Length_m0B242FFD9866C8C2E8312D323377912462342581,
	QosResponse_get_LatencyMs_m6D92DA16A56CC151961BCC6E090131C2097BF5FC,
	QosResponse_Recv_mC619EA992F44794C22AE151014FAAA8B5CE0E204,
	QosResponse_Verify_mCC3F2D873A922CFD8A9DE72F64E3D20F1958E369,
	QosResponse_ParseFlowControl_mA247FCC942E7322C2CC02B3584FF871AEB0D4F2B,
	QosResponse__ctor_m209C46EAA2D73534740EDB232775F5E32E8DBA4D,
	UcgQosServer_ToString_mA7BEF62CE3005CD01054E082587C558D6C031AE8,
	Configuration__ctor_mFF1BCF6303D8E5E362C3655F302DD9E8870BCD33,
	Configuration_MergeConfigurations_m10AD4EC118B0BF363E4C1993A490032DC0CE981D,
	QosResults__ctor_m6AD651FDED7000ABD5E4B91076A06D599A64F78B,
	QosResults_GetSortedQosResultsAsync_mFEBE0655FD611148054C48B89D5909160C1F5311,
	Response_get_Headers_m61DA9ECA3129CE732D2359F5B59B9BFBA86E6744,
	Response_get_Status_mE999C886D428C9253900FE67E14827DCB10D8B01,
	Response_set_Status_mBF047132804F44DADB37CCA4D2A5CDB3602AAC79,
	Response__ctor_mC03DB957D3366BECC35CB74B16E0756A07E6CB55,
	NULL,
	NULL,
	QosDiscoveryService_get_Instance_m0BC7CD7D6BE39F77087A5B7322D7D3D69F012483,
	QosDiscoveryService_set_Instance_m67A235B23405CEEC8D9B28055A34B18A089E81AA,
	NULL,
	NULL,
	NULL,
	NULL,
	QosPackageInitializer_Register_m296754B1BE6F04E0FDCE27B11D8FAD3F660ACD70,
	QosPackageInitializer_Register_m03F66F4C3694C6000ACFE631EB2AA0C3B8B741D2,
	QosPackageInitializer_Initialize_m31AE4939EEC42056FBC52EB1885A66161909312C,
	QosPackageInitializer_GetHost_m411B2841F662CD20FE22DB9F61BCD9CE392563FA,
	QosPackageInitializer__ctor_mB0FBE994896B3205A0F3F31493AB2958391C088A,
	InternalQosDiscoveryService__ctor_m4B669E8B21EB913A51D88F1250D898A326EC08A7,
	InternalQosDiscoveryService_get_QosDiscoveryApi_mD69672FD2DB905D120F004AC9053987010DDC3A0,
	InternalQosDiscoveryService_set_QosDiscoveryApi_m11DE640172B48DF8EBFB3F82808D795F41C6E524,
	InternalQosDiscoveryService_get_Configuration_mA0DD4210EE91DC1A3D859A9739905D7A927F60D0,
	InternalQosDiscoveryService_set_Configuration_m217D3AA86C388DD9373322CE07C7393FC1832264,
	QosService_get_Instance_m7CE84EAAA9BFA0CE64E1C1C114A669ED706243FC,
	QosService_set_Instance_m10C3C8DF77A585F6F3A45EC8320155D2B98BFCA0,
	NULL,
	NULL,
	NULL,
	NULL,
	WrappedQosService__ctor_m81A14C7E7AA366C58E9FFF014828EBD61FF11CBA,
	WrappedQosService_GetSortedQosResultsAsync_m6FF38AB3A4BE3D3C81822413DAF34E4E2C356DEF,
	WrappedQosService_GetSortedInternalQosResultsAsync_mA47A99113119329B564320BFAB1325B490670F57,
	WrappedQosService_SortResults_m2AC6413067175DCF1399CF82570FEBEBFCD90890,
	WrappedQosService_SendResultMetrics_mF81ACBE0E468F70FC816FBECBC61FB76596CF071,
	WrappedQosService_MapToPublicQosResult_mE680B3A1FF2741E4DA4663D37BD0DD5602AF3235,
	U3CGetSortedQosResultsAsyncU3Ed__13_MoveNext_m3A1785964F213FD44A0500D6EFAAD84E54A76E66,
	U3CGetSortedQosResultsAsyncU3Ed__13_SetStateMachine_m299AEC49C87C4A3498650B4954D477D2EF83F34A,
	U3CGetSortedInternalQosResultsAsyncU3Ed__14_MoveNext_m5CF9319E26D43DB9FE32CB0887CF6B212BF37B01,
	U3CGetSortedInternalQosResultsAsyncU3Ed__14_SetStateMachine_mC8A0A6BEADA8763C7869554817A2D82D3826EEDB,
	U3CU3Ec__cctor_mAA0A9893E2022F1D0029C2E7A8EF3F3D1BB80548,
	U3CU3Ec__ctor_m5D76CBDAAD00C935B111C3EF84C85770C592F4AB,
	U3CU3Ec_U3CSortResultsU3Eb__15_0_m2B8F7BB79D34848D520BDABEBCC8E5709A83CFC4,
	U3CU3Ec_U3CSortResultsU3Eb__15_1_mFA4B96B7622491ABAE392DEED2609383BFD1AD9C,
	QosResult__ctor_mA7BAE5428A353A31F1F4E9424A78B3FC99706C9C,
	QosResult_get_Region_mF3A9270B69D29876DDCB738238875CB8FBEF357D,
	QosResult_get_AverageLatencyMs_mD768392DF931372CFEADC93BFDB4F3402E83884E,
	QosResult_get_PacketLossPercent_m2EE3AABDE7587BEB9CEFEBB352BD23A98F3E1B6F,
	UnsupportedEditorVersionException__ctor_m8647299E324884409566642AEEEC36729FA5F07A,
	UnsupportedEditorVersionException__ctor_mC11FE9277AF1738CAF4411DBD37F10AA9D08B5AD,
	EngineStateHelper_Init_mCD239B6552B997DD76949AEF864CF813A6BDB6FD,
	ThreadHelper_get_SynchronizationContext_mEBBC5F6DC87B64616443CEC47B262855D07E3F3F,
	ThreadHelper_get_TaskScheduler_mE4BB459B45461BD9314E69B218A9D3F021E98672,
	ThreadHelper_get_MainThreadId_mCD0E6B56C919EE955A6854D09DE35BDD964B4027,
	ThreadHelper_Init_m06C176E2197E46ADA5299D3B56CB0DAF6A996525,
	QosJobProvider__ctor_mF18C33B6615A7DB6BB7D499EBF39C5A4C0E6BA86,
	QosJobProvider_Invoke_mCB0FA4045A9C73CF90E75AD05B0941BFBB502A7E,
	QosJobProvider_BeginInvoke_mE3401CEFB56B3A3966ECA906FACBB7521970B7D4,
	QosJobProvider_EndInvoke_mC04093465A5A7837DDD88EF683C5442E0CE38FA9,
	DnsResolver__ctor_m8190EBA2601E2FE2EB896BA1B4F3D1C26BF5DB15,
	DnsResolver_Invoke_m6BD09E67240C78EF4CEEBA58370775BE7B198520,
	DnsResolver_BeginInvoke_mF04E381AFD35B6CBCEB317025B52F7EC63FDE8A4,
	DnsResolver_EndInvoke_mEDC9BF3A6C20588EE374D639BFDC4A01EB3839A2,
	BaselibQosRunner__ctor_mD170788D00E9722B9479E4FF4440D4161A4D43E6,
	BaselibQosRunner_MeasureQosAsync_m6EDEEF2BA140E318C908BF8DF5CC1BA194AF6DB8,
	BaselibQosRunner_ToUcgFormat_m1419968ECB7ECDB7B32C74DB6CBBF1CF61B2FA89,
	BaselibQosRunner_ParseResults_mF971BD8AA0A60F083F10C21B1FBB6322B23FA80A,
	U3CU3Ec__cctor_m26C8EDF69C0D13638AD94C6488ED58BD79C95C7F,
	U3CU3Ec__ctor_m53A1925C62020D1466BB7427BA0A020BD5E7B040,
	U3CU3Ec_U3C_ctorU3Eb__2_0_mF20635D6BECBC77DD68D7933D34C74E53BF39651,
	U3CU3Ec_U3CMeasureQosAsyncU3Eb__3_0_mEF244A574C993EC47234D5203EBF0F9A2D32A2A0,
	U3CU3Ec_U3CMeasureQosAsyncU3Eb__3_1_mDDF2659E48C4F06482DCC87A0908A9A19C2168C2,
	U3CMeasureQosAsyncU3Ed__3_MoveNext_mDCDD25826B3494B3AE4241AE8A3AD5F8DFB2A29C,
	U3CMeasureQosAsyncU3Ed__3_SetStateMachine_m1D0DB6B0A212BD39D5B0D6E603F6DD02825170EE,
	U3CToUcgFormatU3Ed__4_MoveNext_m0ECDBA162DD2D83E2B015AD598F48967099F2B98,
	U3CToUcgFormatU3Ed__4_SetStateMachine_mF1C11985D692E30EBB0C4B9802D4CA064A023CEB,
	NULL,
	NULL,
	NULL,
	NULL,
	ErrorResponseBody__ctor_mD1405A130D7BC0164F06C0B1431A3E555CA39800,
	ErrorResponseBody_get_Type_mEEF34D6A7A21C9D856E2658F65E38CF8EB2679BA,
	ErrorResponseBody_get_Title_mFAAD11466E8D4379B8D5A6886728D125963E866A,
	ErrorResponseBody_get_Status_m875015E553B6BCD878A780B184DFC037B4709807,
	ErrorResponseBody_get_Code_mDB11C04F90C5B4F4EB37657ED8DC999A1002D100,
	ErrorResponseBody_get_Detail_m685F0F780303CEDD3E219878ABBCDAB71D49DA16,
	ErrorResponseBody_get_Instance_m0DBA78EB41CD0AE3A218F39D4D03DD9783F7D05B,
	ErrorResponseBody_get_Details_m23800634EE62A708C6F4B020B259766ADE9EA048,
	KeyValuePair__ctor_mA8C81C1804EAA717255BA0B43B9030EC37B77448,
	KeyValuePair_get_Key_mF923D1589D475573BBC8FA490E960667AB1F756A,
	KeyValuePair_get_Value_m50CA3CC82B0C2A2FA87F600205DD564024461CFE,
	NULL,
	NULL,
	QosServer__ctor_mC629272624FD8B341620B53A52EF8C535E3535DC,
	QosServer_get_Endpoints_mD617719F90188011B22A4FA9C155299FE9CDB703,
	QosServer_get_Region_mD34E1FEF6D71BA6B31A8E1670F01E516FF173141,
	QosServer_get_Services_mAF031EDF95DF0D7C94917C41468795BDA553797B,
	QosServersList__ctor_m2BF251BB54BEA0FA96F0EFD72EF6748DF6703479,
	QosServersList_get_Servers_m50BE80B5A162F0A4CEBED1C13999395CDC32E794,
	QosServersResponseBody__ctor_m2298B57029DEEBB06027B40E1193737DEE9801DD,
	QosServersResponseBody_get_Data_mE5664C85DA17FC35E4BB7FE93E65FDD046FE0E71,
	BaseApiClient__ctor_m99B4C250174C7CCEF6640C19E082E9FEF4DF0F99,
	DeserializationException__ctor_m9CC49208201EB2245A19CB9111E48E0C9AAD9AFC,
	DeserializationException__ctor_m969A27E83C9B37A47D137A5E4B7B9F7BBA4F5330,
	DeserializationException__ctor_mDBCD4FBA44EFF08B6C206A15B9F14C34BEC18F98,
	DeserializationSettings__ctor_m82FDFFCE4B4F48D1131AAD21C5B87C5E5DFD2F0D,
	HttpClient__ctor_m280356AE0B5577C3BF6544E6673E70C13458016A,
	HttpClient_MakeRequestAsync_m59AEFEF16C8062494F149D5D8EF24EEF5951C138,
	HttpClient_MakeRequestAsync_m50DC00F79426CC78DC03EA6E492D2224A2D27F30,
	HttpClient_ProcessRequest_mE537ED13A9B5BCC060DAA8B33F2038677A899098,
	HttpClient_CreateWebRequest_m32466AF8B4E31091CB738CAFA88C8F42FCDD686E,
	HttpClient_CreateWebRequestAsync_mE29C3A2AD414500FB9B360CEA834FA802957E413,
	HttpClient_CreateWebRequestAsync_m7A2A754949C98CCAECD0D937602197F226EE3586,
	HttpClient_SetupMultipartRequest_m942E9984424A47309D6CEC78FA2B679EEB0D3D90,
	HttpClient_CreateHttpClientResponse_m1CF6C4DE6D8B11053BD0569304901C5A72624B10,
	HttpClient__cctor_m81F42CB474E726969980761A454A6DE4F6006201,
	HttpClient_U3CProcessRequestU3Eg__ShouldRetryU7C5_1_m3FF51D7C80DF0578391FD07EC6365CCD2B12E576,
	U3CMakeRequestAsyncU3Ed__3_MoveNext_m46149872A41451C0459335243024746705EC7CB5,
	U3CMakeRequestAsyncU3Ed__3_SetStateMachine_m76B718098FE85BF2B623BBE1C88AB0EE1484612D,
	U3CMakeRequestAsyncU3Ed__4_MoveNext_mC01E6A0864677EBA487FA6E848EBAD9ADD8D2AF4,
	U3CMakeRequestAsyncU3Ed__4_SetStateMachine_mE23586ACBF3E089A1599DE1D155480B489C9E913,
	U3CU3Ec__DisplayClass5_0__ctor_m40692977886B6F2A77F04D996EF600E964D2DD63,
	U3CU3Ec__DisplayClass5_0_U3CProcessRequestU3Eg__SetupRequestU7C0_m2123AF8E03516D225FC5F04D5AFC8A694C86261B,
	U3CU3Ec__DisplayClass5_0_U3CProcessRequestU3Eg__AsyncOpCompletedU7C2_m47623757DD2D730990359F8BBB3E3DFB6684F67B,
	U3CProcessRequestU3Ed__5__ctor_m159E20D0FE626ECBA99229CCAD562725C077E107,
	U3CProcessRequestU3Ed__5_System_IDisposable_Dispose_mE4B58F85188635F8756520E72896A58F228A04AB,
	U3CProcessRequestU3Ed__5_MoveNext_mC6C2BAC5A58D2C4218D0637E065DDA70A658D07F,
	U3CProcessRequestU3Ed__5_System_Collections_Generic_IEnumeratorU3CSystem_ObjectU3E_get_Current_m63DFD04C43BA81FAFCECD61D026A1C9CDD11AC86,
	U3CProcessRequestU3Ed__5_System_Collections_IEnumerator_Reset_m48F7D010406A28FFE172D01326774E590C4994CD,
	U3CProcessRequestU3Ed__5_System_Collections_IEnumerator_get_Current_m0F3CD482F89F40563CBB07A4B5DACAFDA668A387,
	U3CU3Ec__DisplayClass7_0__ctor_mB57252D69145549A05B7AC369DB36B5C22A51D3A,
	U3CU3Ec__DisplayClass7_0_U3CCreateWebRequestAsyncU3Eb__0_mE0836D216B45384B3487278240EB02DD51FD5BDD,
	U3CU3CCreateWebRequestAsyncU3Eb__0U3Ed_MoveNext_mD060FEB8287E76472F4F91733E874C4702E787B5,
	U3CU3CCreateWebRequestAsyncU3Eb__0U3Ed_SetStateMachine_m4ABB7FBDA2D9CB66079338761CD944DE4C9A603F,
	U3CCreateWebRequestAsyncU3Ed__7_MoveNext_mF01B9C370C7CFE15812010FA434DC2D5689B9586,
	U3CCreateWebRequestAsyncU3Ed__7_SetStateMachine_mB2F0C29A29F66A7401BEF44EB6694437FD707ED1,
	U3CU3Ec__DisplayClass8_0__ctor_m9045A2D19FC765ACFB746A91F9C83040A6496DA5,
	U3CU3Ec__DisplayClass8_0_U3CCreateWebRequestAsyncU3Eb__0_m219E2AF1C9B724DD612275B1FA10C78F144E65EF,
	U3CU3CCreateWebRequestAsyncU3Eb__0U3Ed_MoveNext_m8934FB8022AE7CF1AB11F06E3CB42A38CE255D13,
	U3CU3CCreateWebRequestAsyncU3Eb__0U3Ed_SetStateMachine_m0FF0BD3CB916FC6CC57BA1813B2F100A7A7FFE85,
	U3CCreateWebRequestAsyncU3Ed__8_MoveNext_mE8324F913699569B8A7885F369C06A1BC51AFE4E,
	U3CCreateWebRequestAsyncU3Ed__8_SetStateMachine_mB1FD0ECA05BACCB6EDBB4A807DD74C21E6EB016A,
	HttpClientResponse__ctor_mD1FC246D901ACC12807D2B5C1DB39B038622A4E0,
	HttpClientResponse_get_Headers_mF870416900DD323B636429C079B0BCAE39AEA2AA,
	HttpClientResponse_get_StatusCode_mC8A80B746A2F6E82F65517C3AB4C479784B55998,
	HttpClientResponse_get_IsHttpError_m6DCA23589DA544E4E462A755FA5E781206995A3B,
	HttpClientResponse_get_IsNetworkError_m3C3F6623CBFEFED95570B0EC1677CA7E31C9DAE0,
	HttpClientResponse_get_Data_mB0A3BB94BF7CE548D1464659194CA4E9557E09DE,
	HttpClientResponse_get_ErrorMessage_mC9BA60D07DC70CF2BAA92C4C256BDF5CB74F326B,
	HttpException__ctor_mCC244F8536EC09FF5682C779CE590A4CCC92083A,
	HttpException__ctor_mB86916F592FA13816761064DA3D9F23C54ACB6F3,
	HttpException__ctor_m776EE74F052257E8AE9ABCF9C16CBF5774673578,
	HttpException__ctor_m4927A27335A8E59162013A85EF8972D88AA0754C,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	JsonHelpers_RegisterTypesForAOT_m974460CCF262A51C0C6A1A6DC8A5DD8C0E65BDB5,
	NULL,
	NULL,
	NULL,
	JsonObject__ctor_m400BFEB71293B1BAE2A146047B091BC8CB26C419,
	JsonObject_GetAsString_m8CAC654D2EBE92E309029903CE9BC57DBB9BE8F3,
	NULL,
	NULL,
	NULL,
	NULL,
	JsonObject_ValidateValue_m3D015321ABD5AE6AED7B475B6D3C34F0BCE63D80,
	JsonObjectConverter_WriteJson_m4443914EDF2F50BD076694458FA416DD5E820385,
	JsonObjectConverter_ReadJson_m15F9F7E014B8BDC97D20CBAC668D406681702479,
	JsonObjectConverter_CanConvert_m671986316D3626EE88F9D7F3FE6FE85F217DCDD9,
	JsonObjectConverter__ctor_m3590310A54D23B98456D5A20348876A06E517477,
	ResponseDeserializationException__ctor_mBB646EAD382A7D28AC499D61C855E3D206D23ED6,
	ResponseDeserializationException__ctor_m704DEEF38F300A4921D3EDD6C564761DFE661C53,
	ResponseDeserializationException__ctor_mE6E1AB43F6B88301B5245AB34E1098448BC0554A,
	ResponseDeserializationException__ctor_m1DE598AF93250D2B52554D9769AC6800E23B124D,
	ResponseDeserializationException__ctor_m6B1A5AAC07DBF1DE306A000ECF99CE9648615C1B,
	ResponseDeserializationException__ctor_mA4DEFE9DC1A572D228890B5512CB493D1282324B,
	NULL,
	ResponseHandler_TryDeserializeResponse_m8B10BE8FAD4A565918C93EBDAF0369E4486AE8D4,
	ResponseHandler_GetDeserializedJson_m677949D173DF6A65ADDAB1AD7FD2B972F3598C08,
	ResponseHandler_HandleAsyncResponse_mF85227D729EBBB63B3F269D558F28D7B3477CB31,
	ResponseHandler_CreateOneOfException_m997402BEBF88FA749E79E2FED79A3FEAFCE53AC7,
	ResponseHandler_CreateHttpException_m48860939F71126F5AC48218C48CA033069D606F8,
	NULL,
	UnityWebRequestHelpers_GetAwaiter_mFE3CEA3273241928556A3EE6EB2D699C01EF52F7,
	UnityWebRequestHelpers_CreateHttpClientResponse_m83A8F9B40812C3017F91315CC83DEA0BE3D74123,
	U3CU3Ec__DisplayClass0_0__ctor_m1A61824C8360274E7FD6DBB60E9D9060BB60DD89,
	U3CU3Ec__DisplayClass0_0_U3CGetAwaiterU3Eb__0_m57EB7F27B683B8FEE80D26546C11F61A1718DF66,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	QosDiscoveryApiBaseRequest_AddParamsToQueryParams_m37EBFF6DDCA6400B0C77F2CABAB42FDFA86B5A97,
	QosDiscoveryApiBaseRequest_AddParamsToQueryParams_m56323A5B31B725034B0CA9AE992F7FB2946AE2FD,
	NULL,
	QosDiscoveryApiBaseRequest_GetPathParamString_m175BA1D606F251D4F8318DF77732D96F9BCFDC7D,
	QosDiscoveryApiBaseRequest_ConstructBody_m7F185A28CF2CCE8BC8E0CBCB25D6AEFEDE0126D0,
	QosDiscoveryApiBaseRequest_ConstructBody_mDD86CD521B7A69DA0C57CFFCF5F0AC6CEDB09817,
	QosDiscoveryApiBaseRequest_ConstructBody_m916EE630F6706AB71EFDFA875D84FD2684CE2C77,
	QosDiscoveryApiBaseRequest_GenerateAcceptHeader_m8393B909E55846FC30E166C3ED7E52CBFA9CFD2F,
	QosDiscoveryApiBaseRequest_GenerateContentTypeHeader_m8578BC7C80BEA3C4919B898356FC5A205B59DCE7,
	QosDiscoveryApiBaseRequest_GenerateMultipartFormFileSection_mBBED2819CA892389B78A9DC3D78E90B2DDFBB1F8,
	QosDiscoveryApiBaseRequest_GenerateMultipartFormFileSection_mD0AF281D370DCAA50A68D266A19600CD6A6263BC,
	QosDiscoveryApiBaseRequest_GetFileName_m060FEF2B62E15D45760509984E2ABD20FF41FC2A,
	QosDiscoveryApiBaseRequest__ctor_m9122E977B93327ACE35337EA6E107C8EBC1C4A6A,
	QosDiscoveryApiBaseRequest__cctor_mB3CFEAA8095B2C5F15A1F5BAD07BB5EE915B4D99,
	GetServersRequest_get_Region_m49F11E7EA0533CD65FB26FBE102104D2338AC934,
	GetServersRequest_get_Service_mD27F2D2FFD7924C9681C2116EFAFD03E77F59FEA,
	GetServersRequest__ctor_m454773232F666315509CCB10A2B5A029BD2CB70D,
	GetServersRequest_ConstructUrl_m34E2AC5428BA279F3BF7001DB96FA0702B9D44C2,
	GetServersRequest_ConstructBody_mCDAF08BF135ED626205192361C7757197C162166,
	GetServersRequest_ConstructHeaders_m8CE30E99E33DDE136776B5830F2FCBF91C0B426A,
	U3CU3Ec__cctor_m9AC91843F1D0511A7FFB24E03AFF31ED8BB08989,
	U3CU3Ec__ctor_m8002077DF6C1A68B5F9A799915DA6F1B9DC5B2FA,
	U3CU3Ec_U3C_ctorU3Eb__7_0_mDF3BC19C96293831FC8ACA1C245D272CC33625A9,
	NULL,
	QosDiscoveryApiClient_get_Configuration_mA8F6B3F90810410D20D6420909967781E524A3FE,
	QosDiscoveryApiClient_set_Configuration_m98B22FA0BDAE4468C4E4B505D0AC6D4448CDFAA5,
	QosDiscoveryApiClient__ctor_mAC0DD995EBE1EE7C9B7BF1D376B15451A3322D11,
	QosDiscoveryApiClient_GetServersAsync_m0ED9B245DB1571CD45BFF48C6A1968C44D2B545B,
	U3CGetServersAsyncU3Ed__7_MoveNext_m7D03C073B6C99374D7B4ED833585D6041991DB07,
	U3CGetServersAsyncU3Ed__7_SetStateMachine_m1A5B420B9D92DA3196FDD52C3069A8DBDCFFD6D8,
};
extern void InternalQosResult_get_AverageLatencyMs_mC4DD63FA694AB15B32ECE7B95C391BF79250708B_AdjustorThunk (void);
extern void InternalQosResult_get_PacketLoss_mAE93BDA8F14001F7D8B9B6B07D7BF5A6492F0E7A_AdjustorThunk (void);
extern void InternalQosResult_AddAggregateLatency_mF4F807BAA262DEAE0E1B5A741C4D4F3B87AD363F_AdjustorThunk (void);
extern void QosJob_get_QosResults_m99D294F773A497B20A7B607B3A5867AB1DD85CD6_AdjustorThunk (void);
extern void QosJob__ctor_mC7757F9FE843176A16749879A99C777DD191D4BB_AdjustorThunk (void);
extern void QosJob_Dispose_m023B42665979401AB7384B2BAB21B090BDDD21DF_AdjustorThunk (void);
extern void QosJob_Execute_m66569856598381E3B9D9461F905CD7DD555819C6_AdjustorThunk (void);
extern void QosJob_ProcessServers_m002469312F12627E8C8179045EF58AAEFC2767B6_AdjustorThunk (void);
extern void QosJob_ProcessServer_m193D982E9EBF657DB785605C7ACFDE9FC1BFB121_AdjustorThunk (void);
extern void QosJob_SendQosRequests_mE07A62D70704ABCBBFFD5C5B0A9E9D2075DC6A1E_AdjustorThunk (void);
extern void QosJob_StoreServer_m10EC014D95822A081040752A7F218E8C3A511A09_AdjustorThunk (void);
extern void QosJob_StoreResult_m4A593D9ECAFD18243069F8F1E01DD5362F5CCFC5_AdjustorThunk (void);
extern void QosJob_RecvQosResponsesTimed_m05465557D4349CA3EB44FFFF643B1C32717301F6_AdjustorThunk (void);
extern void QosJob_RecvQosResponses_mF7D285B242196414A29B2EB92995C41D5EF0E9CC_AdjustorThunk (void);
extern void QosJob_EnableReceiveWait_m0411682DA5665E33783F6DE96212DEB4F369FC43_AdjustorThunk (void);
extern void QosJob_LookupResult_mF78311CDB65FB597D261547BD872FBBB99D0D43E_AdjustorThunk (void);
extern void InternalQosServer__ctor_mDFFD17B79B32982F24BBF7814A2238B3A8DC1AA2_AdjustorThunk (void);
extern void InternalQosServer_get_FirstIdx_mE1E26F2B9C43302D4D3A669677A32BB4D2CE0478_AdjustorThunk (void);
extern void InternalQosServer_set_FirstIdx_mEE8FDA205B7B8B2DE6E8C6F1D0BFF888C1434A28_AdjustorThunk (void);
extern void InternalQosServer_get_RequestIdentifier_m5BB7DCEB91EAD25DBD1733A74B42F4B490CCA2D5_AdjustorThunk (void);
extern void InternalQosServer_set_RequestIdentifier_mAA9EF4C5C34AFB4EA335CBF999D2FBB46BD15E1F_AdjustorThunk (void);
extern void InternalQosServer_get_Duplicate_mC9F0B30BEC3CEC496735613D59181989F13DEF2B_AdjustorThunk (void);
extern void InternalQosServer_get_Address_mAF7047289348E24CA18A4202DA79D49C2E2298E4_AdjustorThunk (void);
extern void NetworkEndPoint_get_Port_m251B69FEFF5FC2CF22A30DB9AA40348F340408CB_AdjustorThunk (void);
extern void NetworkEndPoint_get_Family_mD35537952F0BCF8578F516DC06DC1D3B225F724F_AdjustorThunk (void);
extern void NetworkEndPoint_get_Address_m1EB1D2DAF46FB52253E311B8423DF10FF8F03FBD_AdjustorThunk (void);
extern void NetworkEndPoint_get_IsValid_m56A53E05D4C28A38048155E597009964FBAE2D94_AdjustorThunk (void);
extern void NetworkEndPoint_AddressAsString_mBF7B5153F6570D362AB7672C08CB24369B78D0E3_AdjustorThunk (void);
extern void UcgQosServer_ToString_mA7BEF62CE3005CD01054E082587C558D6C031AE8_AdjustorThunk (void);
extern void U3CGetSortedQosResultsAsyncU3Ed__13_MoveNext_m3A1785964F213FD44A0500D6EFAAD84E54A76E66_AdjustorThunk (void);
extern void U3CGetSortedQosResultsAsyncU3Ed__13_SetStateMachine_m299AEC49C87C4A3498650B4954D477D2EF83F34A_AdjustorThunk (void);
extern void U3CGetSortedInternalQosResultsAsyncU3Ed__14_MoveNext_m5CF9319E26D43DB9FE32CB0887CF6B212BF37B01_AdjustorThunk (void);
extern void U3CGetSortedInternalQosResultsAsyncU3Ed__14_SetStateMachine_mC8A0A6BEADA8763C7869554817A2D82D3826EEDB_AdjustorThunk (void);
extern void U3CMeasureQosAsyncU3Ed__3_MoveNext_mDCDD25826B3494B3AE4241AE8A3AD5F8DFB2A29C_AdjustorThunk (void);
extern void U3CMeasureQosAsyncU3Ed__3_SetStateMachine_m1D0DB6B0A212BD39D5B0D6E603F6DD02825170EE_AdjustorThunk (void);
extern void U3CToUcgFormatU3Ed__4_MoveNext_m0ECDBA162DD2D83E2B015AD598F48967099F2B98_AdjustorThunk (void);
extern void U3CToUcgFormatU3Ed__4_SetStateMachine_mF1C11985D692E30EBB0C4B9802D4CA064A023CEB_AdjustorThunk (void);
extern void U3CMakeRequestAsyncU3Ed__3_MoveNext_m46149872A41451C0459335243024746705EC7CB5_AdjustorThunk (void);
extern void U3CMakeRequestAsyncU3Ed__3_SetStateMachine_m76B718098FE85BF2B623BBE1C88AB0EE1484612D_AdjustorThunk (void);
extern void U3CMakeRequestAsyncU3Ed__4_MoveNext_mC01E6A0864677EBA487FA6E848EBAD9ADD8D2AF4_AdjustorThunk (void);
extern void U3CMakeRequestAsyncU3Ed__4_SetStateMachine_mE23586ACBF3E089A1599DE1D155480B489C9E913_AdjustorThunk (void);
extern void U3CU3CCreateWebRequestAsyncU3Eb__0U3Ed_MoveNext_mD060FEB8287E76472F4F91733E874C4702E787B5_AdjustorThunk (void);
extern void U3CU3CCreateWebRequestAsyncU3Eb__0U3Ed_SetStateMachine_m4ABB7FBDA2D9CB66079338761CD944DE4C9A603F_AdjustorThunk (void);
extern void U3CCreateWebRequestAsyncU3Ed__7_MoveNext_mF01B9C370C7CFE15812010FA434DC2D5689B9586_AdjustorThunk (void);
extern void U3CCreateWebRequestAsyncU3Ed__7_SetStateMachine_mB2F0C29A29F66A7401BEF44EB6694437FD707ED1_AdjustorThunk (void);
extern void U3CU3CCreateWebRequestAsyncU3Eb__0U3Ed_MoveNext_m8934FB8022AE7CF1AB11F06E3CB42A38CE255D13_AdjustorThunk (void);
extern void U3CU3CCreateWebRequestAsyncU3Eb__0U3Ed_SetStateMachine_m0FF0BD3CB916FC6CC57BA1813B2F100A7A7FFE85_AdjustorThunk (void);
extern void U3CCreateWebRequestAsyncU3Ed__8_MoveNext_mE8324F913699569B8A7885F369C06A1BC51AFE4E_AdjustorThunk (void);
extern void U3CCreateWebRequestAsyncU3Ed__8_SetStateMachine_mB1FD0ECA05BACCB6EDBB4A807DD74C21E6EB016A_AdjustorThunk (void);
extern void U3CGetServersAsyncU3Ed__7_MoveNext_m7D03C073B6C99374D7B4ED833585D6041991DB07_AdjustorThunk (void);
extern void U3CGetServersAsyncU3Ed__7_SetStateMachine_m1A5B420B9D92DA3196FDD52C3069A8DBDCFFD6D8_AdjustorThunk (void);
static Il2CppTokenAdjustorThunkPair s_adjustorThunks[51] = 
{
	{ 0x06000001, InternalQosResult_get_AverageLatencyMs_mC4DD63FA694AB15B32ECE7B95C391BF79250708B_AdjustorThunk },
	{ 0x06000002, InternalQosResult_get_PacketLoss_mAE93BDA8F14001F7D8B9B6B07D7BF5A6492F0E7A_AdjustorThunk },
	{ 0x06000003, InternalQosResult_AddAggregateLatency_mF4F807BAA262DEAE0E1B5A741C4D4F3B87AD363F_AdjustorThunk },
	{ 0x06000005, QosJob_get_QosResults_m99D294F773A497B20A7B607B3A5867AB1DD85CD6_AdjustorThunk },
	{ 0x06000006, QosJob__ctor_mC7757F9FE843176A16749879A99C777DD191D4BB_AdjustorThunk },
	{ 0x06000007, QosJob_Dispose_m023B42665979401AB7384B2BAB21B090BDDD21DF_AdjustorThunk },
	{ 0x06000008, QosJob_Execute_m66569856598381E3B9D9461F905CD7DD555819C6_AdjustorThunk },
	{ 0x06000009, QosJob_ProcessServers_m002469312F12627E8C8179045EF58AAEFC2767B6_AdjustorThunk },
	{ 0x0600000A, QosJob_ProcessServer_m193D982E9EBF657DB785605C7ACFDE9FC1BFB121_AdjustorThunk },
	{ 0x0600000B, QosJob_SendQosRequests_mE07A62D70704ABCBBFFD5C5B0A9E9D2075DC6A1E_AdjustorThunk },
	{ 0x0600000C, QosJob_StoreServer_m10EC014D95822A081040752A7F218E8C3A511A09_AdjustorThunk },
	{ 0x0600000D, QosJob_StoreResult_m4A593D9ECAFD18243069F8F1E01DD5362F5CCFC5_AdjustorThunk },
	{ 0x0600000E, QosJob_RecvQosResponsesTimed_m05465557D4349CA3EB44FFFF643B1C32717301F6_AdjustorThunk },
	{ 0x0600000F, QosJob_RecvQosResponses_mF7D285B242196414A29B2EB92995C41D5EF0E9CC_AdjustorThunk },
	{ 0x06000010, QosJob_EnableReceiveWait_m0411682DA5665E33783F6DE96212DEB4F369FC43_AdjustorThunk },
	{ 0x06000011, QosJob_LookupResult_mF78311CDB65FB597D261547BD872FBBB99D0D43E_AdjustorThunk },
	{ 0x06000013, InternalQosServer__ctor_mDFFD17B79B32982F24BBF7814A2238B3A8DC1AA2_AdjustorThunk },
	{ 0x06000014, InternalQosServer_get_FirstIdx_mE1E26F2B9C43302D4D3A669677A32BB4D2CE0478_AdjustorThunk },
	{ 0x06000015, InternalQosServer_set_FirstIdx_mEE8FDA205B7B8B2DE6E8C6F1D0BFF888C1434A28_AdjustorThunk },
	{ 0x06000016, InternalQosServer_get_RequestIdentifier_m5BB7DCEB91EAD25DBD1733A74B42F4B490CCA2D5_AdjustorThunk },
	{ 0x06000017, InternalQosServer_set_RequestIdentifier_mAA9EF4C5C34AFB4EA335CBF999D2FBB46BD15E1F_AdjustorThunk },
	{ 0x06000018, InternalQosServer_get_Duplicate_mC9F0B30BEC3CEC496735613D59181989F13DEF2B_AdjustorThunk },
	{ 0x06000019, InternalQosServer_get_Address_mAF7047289348E24CA18A4202DA79D49C2E2298E4_AdjustorThunk },
	{ 0x0600001A, NetworkEndPoint_get_Port_m251B69FEFF5FC2CF22A30DB9AA40348F340408CB_AdjustorThunk },
	{ 0x0600001B, NetworkEndPoint_get_Family_mD35537952F0BCF8578F516DC06DC1D3B225F724F_AdjustorThunk },
	{ 0x0600001C, NetworkEndPoint_get_Address_m1EB1D2DAF46FB52253E311B8423DF10FF8F03FBD_AdjustorThunk },
	{ 0x0600001D, NetworkEndPoint_get_IsValid_m56A53E05D4C28A38048155E597009964FBAE2D94_AdjustorThunk },
	{ 0x0600001F, NetworkEndPoint_AddressAsString_mBF7B5153F6570D362AB7672C08CB24369B78D0E3_AdjustorThunk },
	{ 0x0600003F, UcgQosServer_ToString_mA7BEF62CE3005CD01054E082587C558D6C031AE8_AdjustorThunk },
	{ 0x06000066, U3CGetSortedQosResultsAsyncU3Ed__13_MoveNext_m3A1785964F213FD44A0500D6EFAAD84E54A76E66_AdjustorThunk },
	{ 0x06000067, U3CGetSortedQosResultsAsyncU3Ed__13_SetStateMachine_m299AEC49C87C4A3498650B4954D477D2EF83F34A_AdjustorThunk },
	{ 0x06000068, U3CGetSortedInternalQosResultsAsyncU3Ed__14_MoveNext_m5CF9319E26D43DB9FE32CB0887CF6B212BF37B01_AdjustorThunk },
	{ 0x06000069, U3CGetSortedInternalQosResultsAsyncU3Ed__14_SetStateMachine_mC8A0A6BEADA8763C7869554817A2D82D3826EEDB_AdjustorThunk },
	{ 0x0600008A, U3CMeasureQosAsyncU3Ed__3_MoveNext_mDCDD25826B3494B3AE4241AE8A3AD5F8DFB2A29C_AdjustorThunk },
	{ 0x0600008B, U3CMeasureQosAsyncU3Ed__3_SetStateMachine_m1D0DB6B0A212BD39D5B0D6E603F6DD02825170EE_AdjustorThunk },
	{ 0x0600008C, U3CToUcgFormatU3Ed__4_MoveNext_m0ECDBA162DD2D83E2B015AD598F48967099F2B98_AdjustorThunk },
	{ 0x0600008D, U3CToUcgFormatU3Ed__4_SetStateMachine_mF1C11985D692E30EBB0C4B9802D4CA064A023CEB_AdjustorThunk },
	{ 0x060000B7, U3CMakeRequestAsyncU3Ed__3_MoveNext_m46149872A41451C0459335243024746705EC7CB5_AdjustorThunk },
	{ 0x060000B8, U3CMakeRequestAsyncU3Ed__3_SetStateMachine_m76B718098FE85BF2B623BBE1C88AB0EE1484612D_AdjustorThunk },
	{ 0x060000B9, U3CMakeRequestAsyncU3Ed__4_MoveNext_mC01E6A0864677EBA487FA6E848EBAD9ADD8D2AF4_AdjustorThunk },
	{ 0x060000BA, U3CMakeRequestAsyncU3Ed__4_SetStateMachine_mE23586ACBF3E089A1599DE1D155480B489C9E913_AdjustorThunk },
	{ 0x060000C6, U3CU3CCreateWebRequestAsyncU3Eb__0U3Ed_MoveNext_mD060FEB8287E76472F4F91733E874C4702E787B5_AdjustorThunk },
	{ 0x060000C7, U3CU3CCreateWebRequestAsyncU3Eb__0U3Ed_SetStateMachine_m4ABB7FBDA2D9CB66079338761CD944DE4C9A603F_AdjustorThunk },
	{ 0x060000C8, U3CCreateWebRequestAsyncU3Ed__7_MoveNext_mF01B9C370C7CFE15812010FA434DC2D5689B9586_AdjustorThunk },
	{ 0x060000C9, U3CCreateWebRequestAsyncU3Ed__7_SetStateMachine_mB2F0C29A29F66A7401BEF44EB6694437FD707ED1_AdjustorThunk },
	{ 0x060000CC, U3CU3CCreateWebRequestAsyncU3Eb__0U3Ed_MoveNext_m8934FB8022AE7CF1AB11F06E3CB42A38CE255D13_AdjustorThunk },
	{ 0x060000CD, U3CU3CCreateWebRequestAsyncU3Eb__0U3Ed_SetStateMachine_m0FF0BD3CB916FC6CC57BA1813B2F100A7A7FFE85_AdjustorThunk },
	{ 0x060000CE, U3CCreateWebRequestAsyncU3Ed__8_MoveNext_mE8324F913699569B8A7885F369C06A1BC51AFE4E_AdjustorThunk },
	{ 0x060000CF, U3CCreateWebRequestAsyncU3Ed__8_SetStateMachine_mB1FD0ECA05BACCB6EDBB4A807DD74C21E6EB016A_AdjustorThunk },
	{ 0x0600013C, U3CGetServersAsyncU3Ed__7_MoveNext_m7D03C073B6C99374D7B4ED833585D6041991DB07_AdjustorThunk },
	{ 0x0600013D, U3CGetServersAsyncU3Ed__7_SetStateMachine_m1A5B420B9D92DA3196FDD52C3069A8DBDCFFD6D8_AdjustorThunk },
};
static const int32_t s_InvokerIndices[317] = 
{
	9526,
	9459,
	7640,
	0,
	8970,
	153,
	9544,
	9544,
	7772,
	4161,
	1724,
	7853,
	3632,
	1370,
	1370,
	9395,
	1706,
	16531,
	1988,
	9345,
	7479,
	9525,
	7639,
	9261,
	9395,
	9525,
	9345,
	9395,
	9261,
	11006,
	9395,
	15104,
	15104,
	14956,
	14934,
	15291,
	9261,
	9261,
	9261,
	9395,
	7525,
	9261,
	7390,
	9525,
	7639,
	9527,
	7641,
	9345,
	1519,
	9544,
	9261,
	9261,
	9261,
	9261,
	9525,
	9527,
	9525,
	9345,
	764,
	2513,
	9226,
	9544,
	9395,
	1372,
	13432,
	7525,
	3069,
	9395,
	9346,
	7480,
	7525,
	0,
	0,
	16592,
	15699,
	0,
	0,
	0,
	0,
	16643,
	7525,
	6602,
	6602,
	9544,
	2039,
	9395,
	7525,
	9395,
	7525,
	16592,
	15699,
	0,
	0,
	0,
	0,
	1448,
	3069,
	3069,
	6602,
	2039,
	6605,
	9544,
	7525,
	9544,
	7525,
	16643,
	9544,
	6204,
	6737,
	2021,
	9395,
	9345,
	9459,
	9544,
	7525,
	16643,
	16592,
	16592,
	16578,
	16643,
	3983,
	3069,
	1239,
	6602,
	3983,
	6602,
	1773,
	6602,
	3987,
	6602,
	6602,
	13432,
	16643,
	9544,
	3069,
	4967,
	6839,
	9544,
	7525,
	9544,
	7525,
	0,
	0,
	0,
	0,
	222,
	9395,
	9395,
	9345,
	9345,
	9395,
	9395,
	9395,
	3987,
	9395,
	9395,
	0,
	0,
	2039,
	9395,
	9395,
	9395,
	7525,
	9395,
	7525,
	9395,
	7525,
	9544,
	7525,
	3987,
	9544,
	9544,
	622,
	293,
	293,
	622,
	622,
	293,
	12146,
	15309,
	16643,
	14943,
	9544,
	7525,
	9544,
	7525,
	9544,
	6593,
	7525,
	7479,
	9544,
	9261,
	9395,
	9544,
	9395,
	9544,
	9395,
	9544,
	7525,
	9544,
	7525,
	9544,
	9395,
	9544,
	7525,
	9544,
	7525,
	349,
	9395,
	9346,
	9261,
	9261,
	9395,
	9395,
	9544,
	7525,
	3987,
	7525,
	0,
	0,
	0,
	0,
	0,
	0,
	16643,
	0,
	0,
	0,
	7525,
	9395,
	0,
	0,
	0,
	0,
	738,
	2039,
	1239,
	5252,
	9544,
	9544,
	7525,
	3987,
	7525,
	3987,
	2039,
	0,
	13432,
	15309,
	13948,
	13432,
	13432,
	0,
	14830,
	15309,
	9544,
	7525,
	0,
	0,
	0,
	0,
	0,
	0,
	0,
	0,
	0,
	0,
	0,
	0,
	0,
	0,
	0,
	0,
	0,
	0,
	0,
	0,
	0,
	0,
	0,
	0,
	0,
	0,
	0,
	0,
	0,
	0,
	0,
	1773,
	621,
	0,
	6602,
	6602,
	6602,
	6602,
	6602,
	6602,
	1773,
	1773,
	6602,
	9544,
	16643,
	9395,
	9395,
	3987,
	6602,
	9395,
	3069,
	16643,
	9544,
	6602,
	0,
	9395,
	7525,
	2039,
	3069,
	9544,
	7525,
};
static const Il2CppTokenRangePair s_rgctxIndices[13] = 
{
	{ 0x02000049, { 21, 10 } },
	{ 0x0200004A, { 31, 16 } },
	{ 0x060000E2, { 0, 4 } },
	{ 0x060000E7, { 4, 2 } },
	{ 0x060000E8, { 6, 4 } },
	{ 0x060000E9, { 10, 2 } },
	{ 0x060000EA, { 12, 2 } },
	{ 0x060000F6, { 14, 3 } },
	{ 0x060000FC, { 17, 2 } },
	{ 0x06000101, { 19, 2 } },
	{ 0x0600011E, { 47, 1 } },
	{ 0x0600011F, { 48, 1 } },
	{ 0x06000122, { 49, 2 } },
};
extern const uint32_t g_rgctx_U3CU3Ec__DisplayClass1_0_1_t9C3D7CF265E4D32768358D804C471BD05A7B9174;
extern const uint32_t g_rgctx_U3CU3Ec__DisplayClass1_0_1__ctor_m62C9F224D3731C5CCFBA3BB09F5959A6082F1DC3;
extern const uint32_t g_rgctx_U3CU3Ec__DisplayClass1_0_1_U3CTryParseJsonU3Eb__0_m0A5F8C59FD622DE58A5790A705734B2EB37E81EF;
extern const uint32_t g_rgctx_JsonConvert_DeserializeObject_TisT_t2CD5BBB62C33AEF0381695B03F5579242379C151_m5707B7864603934617A9D6782510E4E5F8EC26D1;
extern const uint32_t g_rgctx_JsonConvert_DeserializeObject_TisT_t34F4909B0C70CAB45BBAD9A005491CC81D7ABB5B_mA21206933B4ADE83618C853C00E8A9A18862A67A;
extern const uint32_t g_rgctx_JsonObject_ValidateObject_TisT_t34F4909B0C70CAB45BBAD9A005491CC81D7ABB5B_mABAD012F5774B6000FA907F7DA1698A804FC35D7;
extern const uint32_t g_rgctx_T_t18A46986166795516EC4D65096A82CCA11648C30;
extern const uint32_t g_rgctx_T_t18A46986166795516EC4D65096A82CCA11648C30;
extern const uint32_t g_rgctx_JsonObject_ValidateFieldInfos_TisT_t18A46986166795516EC4D65096A82CCA11648C30_m4669DADA48B871D36C1733477D42145C0A5C2E55;
extern const uint32_t g_rgctx_JsonObject_ValidatePropertyInfos_TisT_t18A46986166795516EC4D65096A82CCA11648C30_m6C8383EB31E2C190AAAB7A401E914D1B110DB5D6;
extern const uint32_t g_rgctx_T_t8CE7E52603B0D1D0B0FE92AF34512F1621B07C4C;
extern const Il2CppRGCTXConstrainedData g_rgctx_T_t8CE7E52603B0D1D0B0FE92AF34512F1621B07C4C_Object_GetType_mE10A8FC1E57F3DF29972CCBC026C2DC3942263B3;
extern const uint32_t g_rgctx_T_tB5CDCD53C784D737481740DC50694E087A5AC11C;
extern const Il2CppRGCTXConstrainedData g_rgctx_T_tB5CDCD53C784D737481740DC50694E087A5AC11C_Object_GetType_mE10A8FC1E57F3DF29972CCBC026C2DC3942263B3;
extern const uint32_t g_rgctx_T_tEB22E3797E54A5241C9AF40C6090FAD03E800423;
extern const uint32_t g_rgctx_T_tEB22E3797E54A5241C9AF40C6090FAD03E800423;
extern const uint32_t g_rgctx_JsonConvert_DeserializeObject_TisT_tEB22E3797E54A5241C9AF40C6090FAD03E800423_m23BBCB5D6DFD3E5C7BAF82C5A191B63C861FA86B;
extern const uint32_t g_rgctx_T_t5009504B50981E281F7AFC4A0CC28DDBEAA15681;
extern const uint32_t g_rgctx_ResponseHandler_TryDeserializeResponse_TisT_t5009504B50981E281F7AFC4A0CC28DDBEAA15681_m0F482CB6404DB9DD00DCA117F0110C863DE2B8DA;
extern const uint32_t g_rgctx_AsyncOpRetry_1_FromCreateAsync_mB852BE70F8E958C2419F1DCCEE5816D94C8CE646;
extern const uint32_t g_rgctx_AsyncOpRetry_1_t93B6B25B1656DD124CF47AF15DFF9121894F234B;
extern const uint32_t g_rgctx_AsyncOpRetry_1_set_CreateOperation_mB38A8F3C184EB5AFAC80A153C3744ECAD3FE30A3;
extern const uint32_t g_rgctx_AsyncOpRetry_1_Pow2_m469FCB4C700D32B34187BFF6B79E793218B1B4AB;
extern const uint32_t g_rgctx_AsyncOpRetry_1_tBC0C2C320B3FC29BB2EE3374CD741452F419B13C;
extern const uint32_t g_rgctx_AsyncOpRetry_1_AddJitter_mDC1A15A0EEBFFC007BA756AC931623C8813ADB88;
extern const uint32_t g_rgctx_AsyncOpRetry_1_tBC0C2C320B3FC29BB2EE3374CD741452F419B13C;
extern const uint32_t g_rgctx_AsyncOpRetry_1__ctor_m2E16917F1E4F28780BB37EBD757EC417D17A6986;
extern const uint32_t g_rgctx_AsyncOpRetry_1_set_RetryCondition_m106B8703A36EB15BBF995A87F153A066A748FEBF;
extern const uint32_t g_rgctx_AsyncOpRetry_1_set_OnComplete_m3F452CBB3B679A382F3808E4D0E476F0BDE4FCAA;
extern const uint32_t g_rgctx_U3CRunU3Ed__35_t3F9A85911862C2864171B164DEAEE8BF06129C0D;
extern const uint32_t g_rgctx_U3CRunU3Ed__35__ctor_m3D6D9A580D59EB0DF0A6DD61E9AA11867F6AD47F;
extern const uint32_t g_rgctx_AsyncOpRetry_1_get_CreateOperation_m1DB7AB53FC1F83D18AAB70DF4995E7BD3CCC2160;
extern const uint32_t g_rgctx_Func_2_tD42BD308AB1626F20A73DA7710E4D2CE896EB5F0;
extern const uint32_t g_rgctx_Func_2_Invoke_m4744A820060FD436A59492D15A2916DCC6D50D95;
extern const uint32_t g_rgctx_T_t7CD00E3D6EFACC602D91CF86A4956CF28122895D;
extern const uint32_t g_rgctx_AsyncOpRetry_1_get_RetryCondition_mF7362A0DB0EA6AD6A78213AC3AE4EAD409D8F3B9;
extern const uint32_t g_rgctx_Func_2_t3EFDF42DEFDED077CF02033479D6D304B04CC791;
extern const uint32_t g_rgctx_Func_2_Invoke_mE50FFE72C5117015C702C128A1E25A76481DDD3F;
extern const uint32_t g_rgctx_AsyncOpRetry_1_get_MaxDelayTime_m2B02E848FEDCD0A73A86B9BD5686390FFDE2F61D;
extern const uint32_t g_rgctx_AsyncOpRetry_1_get_DelayScale_m872809ED2D03FF2CC018916C8A89521E9853EB55;
extern const uint32_t g_rgctx_AsyncOpRetry_1_get_JitterMagnitude_mF26B8A834A78A3E8E1784D8B51D9D641A8A8122B;
extern const uint32_t g_rgctx_AsyncOpRetry_1_CalculateDelay_m7F351362B84961EFEE02A3A5B8F8DBA8BA6D831F;
extern const uint32_t g_rgctx_AsyncOpRetry_1_t00AFD30B1F81D3693B8792ADF7668B97421C7D32;
extern const uint32_t g_rgctx_AsyncOpRetry_1_get_MaxRetries_mE73CCE934B24E7EB4F9F6D6FDCA74E19BB3B8755;
extern const uint32_t g_rgctx_AsyncOpRetry_1_get_OnComplete_mBBF6C858DA82845448A992BFD9BF1EDCD64240E1;
extern const uint32_t g_rgctx_Action_1_t9A359FDFC54491E7524793CF5CF2A52250E80230;
extern const uint32_t g_rgctx_Action_1_Invoke_m6B57666E1B8AEE45B157F585E38C15CE91634654;
extern const uint32_t g_rgctx_JsonSerialization_SerializeToString_TisT_t9D722A8BBE05F883476B46F8EFA7C4C20DE38EB9_m383CF030A0FB926B44B9D40A142B8B0E54FBF5E4;
extern const uint32_t g_rgctx_T_tDF00C05016CE4301212A0EA91586FF0C1EC1753E;
extern const uint32_t g_rgctx_T_tAB4A1F23625F3F67F8AAD51AE35D1984C1C31480;
extern const Il2CppRGCTXConstrainedData g_rgctx_T_tAB4A1F23625F3F67F8AAD51AE35D1984C1C31480_Object_ToString_mF8AC1EB9D85AB52EC8FD8B8BDD131E855E69673F;
static const Il2CppRGCTXDefinition s_rgctxValues[51] = 
{
	{ (Il2CppRGCTXDataType)2, (const void *)&g_rgctx_U3CU3Ec__DisplayClass1_0_1_t9C3D7CF265E4D32768358D804C471BD05A7B9174 },
	{ (Il2CppRGCTXDataType)3, (const void *)&g_rgctx_U3CU3Ec__DisplayClass1_0_1__ctor_m62C9F224D3731C5CCFBA3BB09F5959A6082F1DC3 },
	{ (Il2CppRGCTXDataType)3, (const void *)&g_rgctx_U3CU3Ec__DisplayClass1_0_1_U3CTryParseJsonU3Eb__0_m0A5F8C59FD622DE58A5790A705734B2EB37E81EF },
	{ (Il2CppRGCTXDataType)3, (const void *)&g_rgctx_JsonConvert_DeserializeObject_TisT_t2CD5BBB62C33AEF0381695B03F5579242379C151_m5707B7864603934617A9D6782510E4E5F8EC26D1 },
	{ (Il2CppRGCTXDataType)3, (const void *)&g_rgctx_JsonConvert_DeserializeObject_TisT_t34F4909B0C70CAB45BBAD9A005491CC81D7ABB5B_mA21206933B4ADE83618C853C00E8A9A18862A67A },
	{ (Il2CppRGCTXDataType)3, (const void *)&g_rgctx_JsonObject_ValidateObject_TisT_t34F4909B0C70CAB45BBAD9A005491CC81D7ABB5B_mABAD012F5774B6000FA907F7DA1698A804FC35D7 },
	{ (Il2CppRGCTXDataType)2, (const void *)&g_rgctx_T_t18A46986166795516EC4D65096A82CCA11648C30 },
	{ (Il2CppRGCTXDataType)1, (const void *)&g_rgctx_T_t18A46986166795516EC4D65096A82CCA11648C30 },
	{ (Il2CppRGCTXDataType)3, (const void *)&g_rgctx_JsonObject_ValidateFieldInfos_TisT_t18A46986166795516EC4D65096A82CCA11648C30_m4669DADA48B871D36C1733477D42145C0A5C2E55 },
	{ (Il2CppRGCTXDataType)3, (const void *)&g_rgctx_JsonObject_ValidatePropertyInfos_TisT_t18A46986166795516EC4D65096A82CCA11648C30_m6C8383EB31E2C190AAAB7A401E914D1B110DB5D6 },
	{ (Il2CppRGCTXDataType)2, (const void *)&g_rgctx_T_t8CE7E52603B0D1D0B0FE92AF34512F1621B07C4C },
	{ (Il2CppRGCTXDataType)5, (const void *)&g_rgctx_T_t8CE7E52603B0D1D0B0FE92AF34512F1621B07C4C_Object_GetType_mE10A8FC1E57F3DF29972CCBC026C2DC3942263B3 },
	{ (Il2CppRGCTXDataType)2, (const void *)&g_rgctx_T_tB5CDCD53C784D737481740DC50694E087A5AC11C },
	{ (Il2CppRGCTXDataType)5, (const void *)&g_rgctx_T_tB5CDCD53C784D737481740DC50694E087A5AC11C_Object_GetType_mE10A8FC1E57F3DF29972CCBC026C2DC3942263B3 },
	{ (Il2CppRGCTXDataType)1, (const void *)&g_rgctx_T_tEB22E3797E54A5241C9AF40C6090FAD03E800423 },
	{ (Il2CppRGCTXDataType)2, (const void *)&g_rgctx_T_tEB22E3797E54A5241C9AF40C6090FAD03E800423 },
	{ (Il2CppRGCTXDataType)3, (const void *)&g_rgctx_JsonConvert_DeserializeObject_TisT_tEB22E3797E54A5241C9AF40C6090FAD03E800423_m23BBCB5D6DFD3E5C7BAF82C5A191B63C861FA86B },
	{ (Il2CppRGCTXDataType)2, (const void *)&g_rgctx_T_t5009504B50981E281F7AFC4A0CC28DDBEAA15681 },
	{ (Il2CppRGCTXDataType)3, (const void *)&g_rgctx_ResponseHandler_TryDeserializeResponse_TisT_t5009504B50981E281F7AFC4A0CC28DDBEAA15681_m0F482CB6404DB9DD00DCA117F0110C863DE2B8DA },
	{ (Il2CppRGCTXDataType)3, (const void *)&g_rgctx_AsyncOpRetry_1_FromCreateAsync_mB852BE70F8E958C2419F1DCCEE5816D94C8CE646 },
	{ (Il2CppRGCTXDataType)2, (const void *)&g_rgctx_AsyncOpRetry_1_t93B6B25B1656DD124CF47AF15DFF9121894F234B },
	{ (Il2CppRGCTXDataType)3, (const void *)&g_rgctx_AsyncOpRetry_1_set_CreateOperation_mB38A8F3C184EB5AFAC80A153C3744ECAD3FE30A3 },
	{ (Il2CppRGCTXDataType)3, (const void *)&g_rgctx_AsyncOpRetry_1_Pow2_m469FCB4C700D32B34187BFF6B79E793218B1B4AB },
	{ (Il2CppRGCTXDataType)2, (const void *)&g_rgctx_AsyncOpRetry_1_tBC0C2C320B3FC29BB2EE3374CD741452F419B13C },
	{ (Il2CppRGCTXDataType)3, (const void *)&g_rgctx_AsyncOpRetry_1_AddJitter_mDC1A15A0EEBFFC007BA756AC931623C8813ADB88 },
	{ (Il2CppRGCTXDataType)2, (const void *)&g_rgctx_AsyncOpRetry_1_tBC0C2C320B3FC29BB2EE3374CD741452F419B13C },
	{ (Il2CppRGCTXDataType)3, (const void *)&g_rgctx_AsyncOpRetry_1__ctor_m2E16917F1E4F28780BB37EBD757EC417D17A6986 },
	{ (Il2CppRGCTXDataType)3, (const void *)&g_rgctx_AsyncOpRetry_1_set_RetryCondition_m106B8703A36EB15BBF995A87F153A066A748FEBF },
	{ (Il2CppRGCTXDataType)3, (const void *)&g_rgctx_AsyncOpRetry_1_set_OnComplete_m3F452CBB3B679A382F3808E4D0E476F0BDE4FCAA },
	{ (Il2CppRGCTXDataType)2, (const void *)&g_rgctx_U3CRunU3Ed__35_t3F9A85911862C2864171B164DEAEE8BF06129C0D },
	{ (Il2CppRGCTXDataType)3, (const void *)&g_rgctx_U3CRunU3Ed__35__ctor_m3D6D9A580D59EB0DF0A6DD61E9AA11867F6AD47F },
	{ (Il2CppRGCTXDataType)3, (const void *)&g_rgctx_AsyncOpRetry_1_get_CreateOperation_m1DB7AB53FC1F83D18AAB70DF4995E7BD3CCC2160 },
	{ (Il2CppRGCTXDataType)2, (const void *)&g_rgctx_Func_2_tD42BD308AB1626F20A73DA7710E4D2CE896EB5F0 },
	{ (Il2CppRGCTXDataType)3, (const void *)&g_rgctx_Func_2_Invoke_m4744A820060FD436A59492D15A2916DCC6D50D95 },
	{ (Il2CppRGCTXDataType)2, (const void *)&g_rgctx_T_t7CD00E3D6EFACC602D91CF86A4956CF28122895D },
	{ (Il2CppRGCTXDataType)3, (const void *)&g_rgctx_AsyncOpRetry_1_get_RetryCondition_mF7362A0DB0EA6AD6A78213AC3AE4EAD409D8F3B9 },
	{ (Il2CppRGCTXDataType)2, (const void *)&g_rgctx_Func_2_t3EFDF42DEFDED077CF02033479D6D304B04CC791 },
	{ (Il2CppRGCTXDataType)3, (const void *)&g_rgctx_Func_2_Invoke_mE50FFE72C5117015C702C128A1E25A76481DDD3F },
	{ (Il2CppRGCTXDataType)3, (const void *)&g_rgctx_AsyncOpRetry_1_get_MaxDelayTime_m2B02E848FEDCD0A73A86B9BD5686390FFDE2F61D },
	{ (Il2CppRGCTXDataType)3, (const void *)&g_rgctx_AsyncOpRetry_1_get_DelayScale_m872809ED2D03FF2CC018916C8A89521E9853EB55 },
	{ (Il2CppRGCTXDataType)3, (const void *)&g_rgctx_AsyncOpRetry_1_get_JitterMagnitude_mF26B8A834A78A3E8E1784D8B51D9D641A8A8122B },
	{ (Il2CppRGCTXDataType)3, (const void *)&g_rgctx_AsyncOpRetry_1_CalculateDelay_m7F351362B84961EFEE02A3A5B8F8DBA8BA6D831F },
	{ (Il2CppRGCTXDataType)2, (const void *)&g_rgctx_AsyncOpRetry_1_t00AFD30B1F81D3693B8792ADF7668B97421C7D32 },
	{ (Il2CppRGCTXDataType)3, (const void *)&g_rgctx_AsyncOpRetry_1_get_MaxRetries_mE73CCE934B24E7EB4F9F6D6FDCA74E19BB3B8755 },
	{ (Il2CppRGCTXDataType)3, (const void *)&g_rgctx_AsyncOpRetry_1_get_OnComplete_mBBF6C858DA82845448A992BFD9BF1EDCD64240E1 },
	{ (Il2CppRGCTXDataType)2, (const void *)&g_rgctx_Action_1_t9A359FDFC54491E7524793CF5CF2A52250E80230 },
	{ (Il2CppRGCTXDataType)3, (const void *)&g_rgctx_Action_1_Invoke_m6B57666E1B8AEE45B157F585E38C15CE91634654 },
	{ (Il2CppRGCTXDataType)3, (const void *)&g_rgctx_JsonSerialization_SerializeToString_TisT_t9D722A8BBE05F883476B46F8EFA7C4C20DE38EB9_m383CF030A0FB926B44B9D40A142B8B0E54FBF5E4 },
	{ (Il2CppRGCTXDataType)2, (const void *)&g_rgctx_T_tDF00C05016CE4301212A0EA91586FF0C1EC1753E },
	{ (Il2CppRGCTXDataType)2, (const void *)&g_rgctx_T_tAB4A1F23625F3F67F8AAD51AE35D1984C1C31480 },
	{ (Il2CppRGCTXDataType)5, (const void *)&g_rgctx_T_tAB4A1F23625F3F67F8AAD51AE35D1984C1C31480_Object_ToString_mF8AC1EB9D85AB52EC8FD8B8BDD131E855E69673F },
};
IL2CPP_EXTERN_C const Il2CppCodeGenModule g_Unity_Services_QoS_CodeGenModule;
const Il2CppCodeGenModule g_Unity_Services_QoS_CodeGenModule = 
{
	"Unity.Services.QoS.dll",
	317,
	s_methodPointers,
	51,
	s_adjustorThunks,
	s_InvokerIndices,
	0,
	NULL,
	13,
	s_rgctxIndices,
	51,
	s_rgctxValues,
	NULL,
	NULL, // module initializer,
	NULL,
	NULL,
	NULL,
};
