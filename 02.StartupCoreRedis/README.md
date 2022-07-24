

## 1.Redis配置-windows

配置 redis 的主从复制，sentinel 高可用，Cluster 集群。

具体程序见：redis\01.redis-windows-7.0.4.4-masterfollower

redis\02.redis-windows-7.0.4.4-sentinel

redis\03.redis-windows-7.0.4.4-cluster

对应redis版本：https://github.com/zkteco-home/redis-windows

注意6.0后，为多线程，替换到程序上要做测试，或者修改 io-threads  

### 1.1 主从复制

#### 1.1.1 配置文件

```shell
port 6379
pidfile /var/run/redis_6379.pid
dir ./db/6379/
#masterauth 1234567890 
requirepass 1234567890
```

```
port 6380
pidfile /var/run/redis_6380.pid
dir ./db/6380/
replicaof 127.0.0.1 6379
masterauth 1234567890 
requirepass 1234567890
```

```
port 6381
pidfile /var/run/redis_6381.pid
dir ./db/6381/
replicaof 127.0.0.1 6379
masterauth 1234567890 
requirepass 1234567890
```

```
slaveof 127.0.0.1 6379 //也可以在某个master实例上，直接运行命令行
```



#### 1.1.2 启动服务

```shell
.\redis-server redis-windows-6379.conf
.\redis-server redis-windows-6380.conf
.\redis-server redis-windows-6381.conf
```



```shell
[023260] 23 Jul 16:08:03.088 # Server initialized
[023260] 23 Jul 16:08:03.089 * Ready to accept connections
[023260] 23 Jul 16:09:14.084 * Replica 127.0.0.1:6380 asks for synchronization
[023260] 23 Jul 16:09:14.084 * Full resync requested by replica 127.0.0.1:6380
[023260] 23 Jul 16:09:14.084 * Replication backlog created, my new replication IDs are '04422e72d0ccb13567e5d7ffb2abac08075e49f4' and '0000000000000000000000000000000000000000'
[023260] 23 Jul 16:09:14.084 * Delay next BGSAVE for diskless SYNC
[023260] 23 Jul 16:09:20.005 * Starting BGSAVE for SYNC with target: replicas sockets
[023260] 23 Jul 16:09:20.053 * Background RDB transfer started by pid 23716
[023260] 23 Jul 16:09:20.190 # fork operation complete
[023260] 23 Jul 16:09:20.206 * Background RDB transfer terminated with success
[023260] 23 Jul 16:09:20.206 * Streamed RDB transfer with replica 127.0.0.1:6380 succeeded (socket). Waiting for REPLCONF ACK from slave to enable streaming
[023260] 23 Jul 16:09:20.206 * Synchronization with replica 127.0.0.1:6380 succeeded
[023260] 23 Jul 17:08:04.019 * 1 changes in 3600 seconds. Saving...
[023260] 23 Jul 17:08:04.508 * Background saving started by pid 24524
[023260] 23 Jul 17:08:04.723 # fork operation complete
[023260] 23 Jul 17:08:04.739 * Background saving terminated with success
[023260] 23 Jul 18:58:15.128 # Disconnecting timedout replica (streaming sync): 127.0.0.1:6380
[023260] 23 Jul 18:58:15.206 # Connection with replica 127.0.0.1:6380 lost.
[023260] 23 Jul 18:58:15.341 * Replica 127.0.0.1:6380 asks for synchronization
[023260] 23 Jul 18:58:15.347 * Partial resynchronization request from 127.0.0.1:6380 accepted. Sending 0 bytes of backlog starting from offset 7461.
[023260] 23 Jul 18:59:16.196 # Connection with replica 127.0.0.1:6380 lost.
[023260] 23 Jul 18:59:16.196 * Replica 127.0.0.1:6380 asks for synchronization
[023260] 23 Jul 18:59:16.196 * Partial resynchronization request from 127.0.0.1:6380 accepted. Sending 84 bytes of backlog starting from offset 7461.
```



```shell
[023260] 23 Jul 16:08:03.088 # Server initialized
[023260] 23 Jul 16:08:03.089 * Ready to accept connections
[023260] 23 Jul 16:09:14.084 * Replica 127.0.0.1:6380 asks for synchronization
[023260] 23 Jul 16:09:14.084 * Full resync requested by replica 127.0.0.1:6380
[023260] 23 Jul 16:09:14.084 * Replication backlog created, my new replication IDs are '04422e72d0ccb13567e5d7ffb2abac08075e49f4' and '0000000000000000000000000000000000000000'
[023260] 23 Jul 16:09:14.084 * Delay next BGSAVE for diskless SYNC
[023260] 23 Jul 16:09:20.005 * Starting BGSAVE for SYNC with target: replicas sockets
[023260] 23 Jul 16:09:20.053 * Background RDB transfer started by pid 23716
[023260] 23 Jul 16:09:20.190 # fork operation complete
[023260] 23 Jul 16:09:20.206 * Background RDB transfer terminated with success
[023260] 23 Jul 16:09:20.206 * Streamed RDB transfer with replica 127.0.0.1:6380 succeeded (socket). Waiting for REPLCONF ACK from slave to enable streaming
[023260] 23 Jul 16:09:20.206 * Synchronization with replica 127.0.0.1:6380 succeeded
[023260] 23 Jul 17:08:04.019 * 1 changes in 3600 seconds. Saving...
[023260] 23 Jul 17:08:04.508 * Background saving started by pid 24524
[023260] 23 Jul 17:08:04.723 # fork operation complete
[023260] 23 Jul 17:08:04.739 * Background saving terminated with success
[023260] 23 Jul 18:58:15.128 # Disconnecting timedout replica (streaming sync): 127.0.0.1:6380
[023260] 23 Jul 18:58:15.206 # Connection with replica 127.0.0.1:6380 lost.
[023260] 23 Jul 18:58:15.341 * Replica 127.0.0.1:6380 asks for synchronization
[023260] 23 Jul 18:58:15.347 * Partial resynchronization request from 127.0.0.1:6380 accepted. Sending 0 bytes of backlog starting from offset 7461.
[023260] 23 Jul 18:59:16.196 # Connection with replica 127.0.0.1:6380 lost.
[023260] 23 Jul 18:59:16.196 * Replica 127.0.0.1:6380 asks for synchronization
[023260] 23 Jul 18:59:16.196 * Partial resynchronization request from 127.0.0.1:6380 accepted. Sending 84 bytes of backlog starting from offset 7461.
```

```shell
[023260] 23 Jul 16:08:03.088 # Server initialized
[023260] 23 Jul 16:08:03.089 * Ready to accept connections
[023260] 23 Jul 16:09:14.084 * Replica 127.0.0.1:6380 asks for synchronization
[023260] 23 Jul 16:09:14.084 * Full resync requested by replica 127.0.0.1:6380
[023260] 23 Jul 16:09:14.084 * Replication backlog created, my new replication IDs are '04422e72d0ccb13567e5d7ffb2abac08075e49f4' and '0000000000000000000000000000000000000000'
[023260] 23 Jul 16:09:14.084 * Delay next BGSAVE for diskless SYNC
[023260] 23 Jul 16:09:20.005 * Starting BGSAVE for SYNC with target: replicas sockets
[023260] 23 Jul 16:09:20.053 * Background RDB transfer started by pid 23716
[023260] 23 Jul 16:09:20.190 # fork operation complete
[023260] 23 Jul 16:09:20.206 * Background RDB transfer terminated with success
[023260] 23 Jul 16:09:20.206 * Streamed RDB transfer with replica 127.0.0.1:6380 succeeded (socket). Waiting for REPLCONF ACK from slave to enable streaming
[023260] 23 Jul 16:09:20.206 * Synchronization with replica 127.0.0.1:6380 succeeded
[023260] 23 Jul 17:08:04.019 * 1 changes in 3600 seconds. Saving...
[023260] 23 Jul 17:08:04.508 * Background saving started by pid 24524
[023260] 23 Jul 17:08:04.723 # fork operation complete
[023260] 23 Jul 17:08:04.739 * Background saving terminated with success
[023260] 23 Jul 18:58:15.128 # Disconnecting timedout replica (streaming sync): 127.0.0.1:6380
[023260] 23 Jul 18:58:15.206 # Connection with replica 127.0.0.1:6380 lost.
[023260] 23 Jul 18:58:15.341 * Replica 127.0.0.1:6380 asks for synchronization
[023260] 23 Jul 18:58:15.347 * Partial resynchronization request from 127.0.0.1:6380 accepted. Sending 0 bytes of backlog starting from offset 7461.
[023260] 23 Jul 18:59:16.196 # Connection with replica 127.0.0.1:6380 lost.
[023260] 23 Jul 18:59:16.196 * Replica 127.0.0.1:6380 asks for synchronization
[023260] 23 Jul 18:59:16.196 * Partial resynchronization request from 127.0.0.1:6380 accepted. Sending 84 bytes of backlog starting from offset 7461.
```



```shell
[023260] 23 Jul 16:08:03.088 # Server initialized
[023260] 23 Jul 16:08:03.089 * Ready to accept connections
[023260] 23 Jul 16:09:14.084 * Replica 127.0.0.1:6380 asks for synchronization
[023260] 23 Jul 16:09:14.084 * Full resync requested by replica 127.0.0.1:6380
[023260] 23 Jul 16:09:14.084 * Replication backlog created, my new replication IDs are '04422e72d0ccb13567e5d7ffb2abac08075e49f4' and '0000000000000000000000000000000000000000'
[023260] 23 Jul 16:09:14.084 * Delay next BGSAVE for diskless SYNC
[023260] 23 Jul 16:09:20.005 * Starting BGSAVE for SYNC with target: replicas sockets
[023260] 23 Jul 16:09:20.053 * Background RDB transfer started by pid 23716
[023260] 23 Jul 16:09:20.190 # fork operation complete
[023260] 23 Jul 16:09:20.206 * Background RDB transfer terminated with success
[023260] 23 Jul 16:09:20.206 * Streamed RDB transfer with replica 127.0.0.1:6380 succeeded (socket). Waiting for REPLCONF ACK from slave to enable streaming
[023260] 23 Jul 16:09:20.206 * Synchronization with replica 127.0.0.1:6380 succeeded
[023260] 23 Jul 17:08:04.019 * 1 changes in 3600 seconds. Saving...
[023260] 23 Jul 17:08:04.508 * Background saving started by pid 24524
[023260] 23 Jul 17:08:04.723 # fork operation complete
[023260] 23 Jul 17:08:04.739 * Background saving terminated with success
[023260] 23 Jul 18:58:15.128 # Disconnecting timedout replica (streaming sync): 127.0.0.1:6380
[023260] 23 Jul 18:58:15.206 # Connection with replica 127.0.0.1:6380 lost.
[023260] 23 Jul 18:58:15.341 * Replica 127.0.0.1:6380 asks for synchronization
[023260] 23 Jul 18:58:15.347 * Partial resynchronization request from 127.0.0.1:6380 accepted. Sending 0 bytes of backlog starting from offset 7461.
[023260] 23 Jul 18:59:16.196 # Connection with replica 127.0.0.1:6380 lost.
[023260] 23 Jul 18:59:16.196 * Replica 127.0.0.1:6380 asks for synchronization
[023260] 23 Jul 18:59:16.196 * Partial resynchronization request from 127.0.0.1:6380 accepted. Sending 84 bytes of backlog starting from offset 7461.
```

#### 1.1.3 测试命令

```shell
127.0.0.1:6379> set port 6379
OK
127.0.0.1:6379> keys *
1) "port"
127.0.0.1:6379> get port
"6379"
127.0.0.1:6379> set ip 127.0.0.3679
OK
127.0.0.1:6379> get ip
"127.0.0.3679"
127.0.0.1:6379> get port ip
```

```
127.0.0.1:6380> keys *
1) "port"
127.0.0.1:6380> get port
"6379"
127.0.0.1:6380> get ip
"127.0.0.3679"
```

```
127.0.0.1:6381> keys *
1) "port"
127.0.0.1:6381> get port
"6379"
127.0.0.1:6381> set ip 11111
(error) READONLY You can't write against a read only replica.
127.0.0.1:6381> get ip
"127.0.0.3679"
```



### 1.2  sentinel 高可用

#### 1.2.1 配置文件

```shell
sentinel myid 8d992c54df8f8677b0b345825f61fb733c73d14d
sentinel deny-scripts-reconfig yes
sentinel monitor redismaster 127.0.0.1 6379 2
sentinel auth-pass redismaster 1234567890
sentinel down-after-milliseconds redismaster 10000
# Generated by CONFIG REWRITE
protected-mode no
port 26379
#user default on nopass sanitize-payload ~* &* +@all
dir "./sentinel/db/"  #注意此处运行后会被修改
```



```shell
sentinel myid 8d992c54df8f8677b0b345825f61fb733c73d14d
sentinel deny-scripts-reconfig yes
sentinel monitor redismaster 127.0.0.1 6379 2
sentinel auth-pass redismaster 1234567890
sentinel down-after-milliseconds redismaster 10000
# Generated by CONFIG REWRITE
protected-mode no
port 26380
#user default on nopass sanitize-payload ~* &* +@all
dir "./sentinel/db/"  #注意此处运行后会被修改
```



```shell
sentinel myid 8d992c54df8f8677b0b345825f61fb733c73d14d
sentinel deny-scripts-reconfig yes
sentinel monitor redismaster 127.0.0.1 6379 2
sentinel auth-pass redismaster 1234567890
sentinel down-after-milliseconds redismaster 10000
# Generated by CONFIG REWRITE
protected-mode no
port 26381
#user default on nopass sanitize-payload ~* &* +@all
dir "./sentinel/db/"  #注意此处运行后会被修改
```



#### 1.2.2 启动服务

在原来的1主2从基础上，启动哨兵模式

```shell
.\redis-server sentinel\redis-sentinel-0.conf --sentinel
.\redis-server sentinel\redis-sentinel-1.conf --sentinel
.\redis-server sentinel\redis-sentinel-2.conf --sentinel
```



```shell
[028736] 23 Jul 23:46:25.655 # Sentinel ID is 8d992c54df8f8677b0b345825f61fb733c73d14c
[028736] 23 Jul 23:46:25.655 # +monitor master redismaster 127.0.0.1 6379 quorum 2
[028736] 23 Jul 23:46:25.702 * +slave slave 127.0.0.1:6380 127.0.0.1 6380 @ redismaster 127.0.0.1 6379
[028736] 23 Jul 23:46:25.702 * Sentinel new configuration saved on disk
[028736] 23 Jul 23:49:14.613 * +sentinel sentinel 8d992c54df8f8677b0b345825f61fb733c73d14d 127.0.0.1 26380 @ redismaster 127.0.0.1 6379
[028736] 23 Jul 23:49:14.613 * Sentinel new configuration saved on disk
[028736] 23 Jul 23:51:33.589 * +sentinel sentinel 8d992c54df8f8677b0b345825f61fb733c73d14e 127.0.0.1 26381 @ redismaster 127.0.0.1 6379
[028736] 23 Jul 23:51:33.620 * Sentinel new configuration saved on disk
```

```shell
[024488] 23 Jul 23:49:12.610 # Sentinel ID is 8d992c54df8f8677b0b345825f61fb733c73d14d
[024488] 23 Jul 23:49:12.610 # +monitor master redismaster 127.0.0.1 6379 quorum 2
[024488] 23 Jul 23:49:12.611 * +slave slave 127.0.0.1:6380 127.0.0.1 6380 @ redismaster 127.0.0.1 6379
[024488] 23 Jul 23:49:12.955 * Sentinel new configuration saved on disk
[024488] 23 Jul 23:49:12.955 * +sentinel sentinel 8d992c54df8f8677b0b345825f61fb733c73d14c 127.0.0.1 26379 @ redismaster 127.0.0.1 6379
[024488] 23 Jul 23:49:13.018 * Sentinel new configuration saved on disk
[024488] 23 Jul 23:51:33.589 * +sentinel sentinel 8d992c54df8f8677b0b345825f61fb733c73d14e 127.0.0.1 26381 @ redismaster 127.0.0.1 6379
[024488] 23 Jul 23:51:33.620 * Sentinel new configuration saved on disk
```

```shell
[019116] 23 Jul 23:51:31.525 # Sentinel ID is 8d992c54df8f8677b0b345825f61fb733c73d14e
[019116] 23 Jul 23:51:31.525 # +monitor master redismaster 127.0.0.1 6379 quorum 2
[019116] 23 Jul 23:51:31.548 * +slave slave 127.0.0.1:6380 127.0.0.1 6380 @ redismaster 127.0.0.1 6379
[019116] 23 Jul 23:51:31.885 * Sentinel new configuration saved on disk
[019116] 23 Jul 23:51:31.885 * +sentinel sentinel 8d992c54df8f8677b0b345825f61fb733c73d14d 127.0.0.1 26380 @ redismaster 127.0.0.1 6379
[019116] 23 Jul 23:51:31.900 * Sentinel new configuration saved on disk
[019116] 23 Jul 23:51:33.230 * +sentinel sentinel 8d992c54df8f8677b0b345825f61fb733c73d14c 127.0.0.1 26379 @ redismaster 127.0.0.1 6379
[019116] 23 Jul 23:51:33.230 * Sentinel new configuration saved on disk
```

#### 1.2.3 测试命令

停用6379，使用config直接配置 1主2从没有办法启动，还是因为2从不全是监听的6379

```shell
[020308] 24 Jul 00:12:20.234 # Sending command to master in replication handshake: -Writing to master: No error
[020308] 24 Jul 00:12:20.375 * Connecting to MASTER 127.0.0.1:6379
[020308] 24 Jul 00:12:20.375 * MASTER <-> REPLICA sync started
[020308] 24 Jul 00:12:22.408 * Non blocking connect for SYNC fired the event.
[020308] 24 Jul 00:12:22.408 # Sending command to master in replication handshake: -Writing to master: No error
[020308] 24 Jul 00:12:22.564 * Connecting to MASTER 127.0.0.1:6379
```

改为全监听6379后成功

```shell
[027160] 24 Jul 00:17:20.563 * Non blocking connect for SYNC fired the event.
[027160] 24 Jul 00:17:20.563 # Sending command to master in replication handshake: -Writing to master: No error
[027160] 24 Jul 00:17:20.735 * Connecting to MASTER 127.0.0.1:6379
[027160] 24 Jul 00:17:20.735 * MASTER <-> REPLICA sync started
[027160] 24 Jul 00:17:22.753 * Non blocking connect for SYNC fired the event.
[027160] 24 Jul 00:17:22.753 # Sending command to master in replication handshake: -Writing to master: No error
[027160] 24 Jul 00:17:22.925 * Connecting to MASTER 127.0.0.1:6379
[027160] 24 Jul 00:17:22.925 * MASTER <-> REPLICA sync started
[027160] 24 Jul 00:17:23.768 * Discarding previously cached master state.
[027160] 24 Jul 00:17:23.768 # Setting secondary replication ID to 311c2836e9277ee28edaab27d790ac9c16440fce, valid up to offset: 32606. New replication ID is f248272b4741de72fb548a0a8d1cfecd05dcfa65
[027160] 24 Jul 00:17:23.768 * MASTER MODE enabled (user request from 'id=15 addr=127.0.0.1:64983 laddr=127.0.0.1:6380 fd=13 name=sentinel-8d992c54-cmd age=26 idle=0 flags=x db=0 sub=0 psub=0 ssub=0 multi=4 qbuf=188 qbuf-free=20286 argv-mem=4 multi-mem=169 rbs=1024 rbp=45 obl=45 oll=0 omem=0 tot-mem=22693 events=r cmd=exec user=default redir=-1 resp=2')
[027160] 24 Jul 00:17:23.783 # CONFIG REWRITE executed with success.
[027160] 24 Jul 00:17:24.221 * Replica 127.0.0.1:6381 asks for synchronization
[027160] 24 Jul 00:17:24.221 * Partial resynchronization request from 127.0.0.1:6381 accepted. Sending 159 bytes of backlog starting from offset 32606.
```

查看6380端口，role:master

```shell
# Replication
role:master
connected_slaves:1
slave0:ip=127.0.0.1,port=6381,state=online,offset=60540,lag=1
master_failover_state:no-failover
master_replid:f248272b4741de72fb548a0a8d1cfecd05dcfa65
master_replid2:311c2836e9277ee28edaab27d790ac9c16440fce
master_repl_offset:60676
second_repl_offset:32606
repl_backlog_active:1
repl_backlog_size:1048576
repl_backlog_first_byte_offset:18032
repl_backlog_histlen:42645

# CPU
used_cpu_sys:0.109375
used_cpu_user:0.109375
used_cpu_sys_children:0.000000
used_cpu_user_children:0.000000

# Modules

# Errorstats
errorstat_NOAUTH:count=3

# Cluster
cluster_enabled:0
```

在6380上新增KEY

```shell
# Keyspace
db0:keys=2,expires=0,avg_ttl=0
127.0.0.1:6380> set aa 11
OK
127.0.0.1:6380>
```

再启动6379，首次报错

```shell
[025912] 24 Jul 00:29:34.457 * (Non critical) Master does not understand REPLCONF listening-port: -NOAUTH Authentication required.
```

修改config

```shell
masterauth 1234567890 
```

再次启动

```shell

[023860] 24 Jul 00:32:44.050 # Server initialized
[023860] 24 Jul 00:32:44.050 * Loading RDB produced by version 7.0.4
[023860] 24 Jul 00:32:44.050 * RDB age 190 seconds
[023860] 24 Jul 00:32:44.050 * RDB memory usage when created 0.89 Mb
[023860] 24 Jul 00:32:44.050 * Done loading RDB, keys loaded: 2, keys expired: 0.
[023860] 24 Jul 00:32:44.050 * DB loaded from disk: 0.000 seconds
[023860] 24 Jul 00:32:44.050 * Before turning into a replica, using my own master parameters to synthesize a cached master: I may be able to synchronize with the new master with just a partial transfer.
[023860] 24 Jul 00:32:44.050 * Ready to accept connections
[023860] 24 Jul 00:32:44.066 * Connecting to MASTER 127.0.0.1:6380
[023860] 24 Jul 00:32:44.066 * MASTER <-> REPLICA sync started
[023860] 24 Jul 00:32:44.066 * Non blocking connect for SYNC fired the event.
[023860] 24 Jul 00:32:44.066 * Master replied to PING, replication can continue...
[023860] 24 Jul 00:32:44.066 * Trying a partial resynchronization (request ae1a082ccb257abc0b8cc0c1c8db992bed64ced9:45249).
[023860] 24 Jul 00:32:49.558 * Full resync from master: f248272b4741de72fb548a0a8d1cfecd05dcfa65:219215
[023860] 24 Jul 00:32:49.746 * MASTER <-> REPLICA sync: receiving streamed RDB from master with EOF to disk
[023860] 24 Jul 00:32:49.746 * Discarding previously cached master state.
[023860] 24 Jul 00:32:49.746 * MASTER <-> REPLICA sync: Flushing old data
[023860] 24 Jul 00:32:49.746 * MASTER <-> REPLICA sync: Loading DB in memory
[023860] 24 Jul 00:32:49.761 * Loading RDB produced by version 7.0.4
[023860] 24 Jul 00:32:49.761 * RDB age 0 seconds
[023860] 24 Jul 00:32:49.761 * RDB memory usage when created 0.00 Mb
[023860] 24 Jul 00:32:49.761 # RDB file was saved with checksum disabled: no check performed.
[023860] 24 Jul 00:32:49.761 * Done loading RDB, keys loaded: 3, keys expired: 0.
[023860] 24 Jul 00:32:49.761 * MASTER <-> REPLICA sync: Finished with success
```

连6379客户端，查看KEY,新增的aa顺利同步

```shell
127.0.0.1:6379> keys *
1) "ip"
2) "port"
3) "aa"
127.0.0.1:6379>
```



### 1.4 Cluster 集群

#### 1.4.1 配置文件

```shell
cluster-enabled yes
cluster-config-file nodes-6379.conf
cluster-node-timeout 15000
appendonly yes

```

#### 1.4.2 安装ruby

**下载ruby安装包，按照指引安装**,安装完成后，运行如下命令

[Downloads (rubyinstaller.org)](https://rubyinstaller.org/downloads/)

```cmd
C:\Windows\system32>gem install redis
Fetching redis-4.7.1.gem
Successfully installed redis-4.7.1
Parsing documentation for redis-4.7.1
Installing ri documentation for redis-4.7.1
Done installing documentation for redis after 1 seconds
1 gem installed
```

#### 1.4.3 启动节点

启动6个节点，使用 start_cluster.bat

```shell
D:\redis\03.redis-windows-7.0.4.4-cluster\6379>title redis7-6379

D:\redis\03.redis-windows-7.0.4.4-cluster\6379>redis-server.exe redis-windows-6379.conf
[015660] 24 Jul 19:10:04.766 # RedisWin is starting ......
[015660] 24 Jul 19:10:04.867 # RedisWin version=7.0.4, bits=64, commit=07/20/2022, modified=0, pid=15660, just started
[015660] 24 Jul 19:10:04.867 # Configuration loaded
[015660] 24 Jul 19:10:04.867 * monotonic clock: POSIX clock_gettime
[015660] 24 Jul 19:10:04.867 * No cluster configuration found, I'm fec9717b267a0db969379f66ef80508334e2360d
                _._
           _.-``__ ''-._
      _.-``    `.  `_.  ''-._           RedisWin 7.0.4 (07/20/2022/0) 64 bit
  .-`` .-```.  ```\/    _.,_ ''-._
 (    '      ,       .-`  | `,    )     Running in cluster mode
 |`-._`-...-` __...-.``-._|'` _.-'|     Port: 6379
 |    `-._   `._    /     _.-'    |     PID: 15660
  `-._    `-._  `-./  _.-'    _.-'
 |`-._`-._    `-.__.-'    _.-'_.-'|
 |    `-._`-._        _.-'_.-'    |
  `-._    `-._`-.__.-'_.-'    _.-'
 |`-._`-._    `-.__.-'    _.-'_.-'|
 |    `-._`-._        _.-'_.-'    |
  `-._    `-._`-.__.-'_.-'    _.-'
      `-._    `-.__.-'    _.-'
          `-._        _.-'
              `-.__.-'

[015660] 24 Jul 19:10:04.898 # Server initialized
[015660] 24 Jul 19:10:04.913 * Creating AOF base file appendonly.aof.1.base.rdb on server start
[015660] 24 Jul 19:10:04.913 * Creating AOF incr file appendonly.aof.1.incr.aof on server start
[015660] 24 Jul 19:10:04.913 * Ready to accept connections
```

#### 1.4.4 启动cluster

```
ruby redis-trib.rb create --replicas 1 127.0.0.1:6379 127.0.0.1:6380 127.0.0.1:6381 127.0.0.1:7379 127.0.0.1:7380 127.0.0.1:7381
#可能出现错误 [ERR] Sorry, can't connect to node 127.0.0.1:6379，是因为redis-trib.rb没指定密码，或者自己修改，可以传参
```

```shell
PS D:\redis\03.redis-windows-7.0.4.4-cluster\6379> ruby redis-trib.rb create --replicas 1 127.0.0.1:6379 127.0.0.1:6380 127.0.0.1:6381 127.0.0.1:7379 127.0.0.1:7380 127.0.0.1:7381
>>> Creating cluster
>>> Performing hash slots allocation on 6 nodes...
Using 3 masters:
127.0.0.1:6379
127.0.0.1:6380
127.0.0.1:6381
Adding replica 127.0.0.1:7379 to 127.0.0.1:6379
Adding replica 127.0.0.1:7380 to 127.0.0.1:6380
Adding replica 127.0.0.1:7381 to 127.0.0.1:6381
M: fec9717b267a0db969379f66ef80508334e2360d 127.0.0.1:6379
   slots:0-5460 (5461 slots) master
M: c70354e26e23cae48490ffc95b83121c060370e1 127.0.0.1:6380
   slots:5461-10922 (5462 slots) master
M: 50ce43368960a8269f103c8d91d8bbf924d66aa3 127.0.0.1:6381
   slots:10923-16383 (5461 slots) master
S: 47e76e89dd8127bbd88500554d955b9d1f189cb7 127.0.0.1:7379
   replicates fec9717b267a0db969379f66ef80508334e2360d
S: a08cad2394ebb3b4f3bb7e0143251fd9c104bad0 127.0.0.1:7380
   replicates c70354e26e23cae48490ffc95b83121c060370e1
S: 3d836a213659018e298f2145ddb9104baa65928d 127.0.0.1:7381
   replicates 50ce43368960a8269f103c8d91d8bbf924d66aa3
Can I set the above configuration? (type 'yes' to accept): >>> Nodes configuration updated
>>> Assign a different config epoch to each node
>>> Sending CLUSTER MEET messages to join the cluster
Waiting for the cluster to join...
>>> Performing Cluster Check (using node 127.0.0.1:6379)
M: fec9717b267a0db969379f66ef80508334e2360d 127.0.0.1:6379
   slots:0-5460 (5461 slots) master
M: c70354e26e23cae48490ffc95b83121c060370e1 127.0.0.1:6380
   slots:5461-10922 (5462 slots) master
M: 50ce43368960a8269f103c8d91d8bbf924d66aa3 127.0.0.1:6381
   slots:10923-16383 (5461 slots) master
M: 47e76e89dd8127bbd88500554d955b9d1f189cb7 127.0.0.1:7379
   slots: (0 slots) master
   replicates fec9717b267a0db969379f66ef80508334e2360d
M: a08cad2394ebb3b4f3bb7e0143251fd9c104bad0 127.0.0.1:7380
   slots: (0 slots) master
   replicates c70354e26e23cae48490ffc95b83121c060370e1
M: 3d836a213659018e298f2145ddb9104baa65928d 127.0.0.1:7381
   slots: (0 slots) master
   replicates 50ce43368960a8269f103c8d91d8bbf924d66aa3
[OK] All nodes agree about slots configuration.
>>> Check for open slots...
>>> Check slots coverage...
[OK] All 16384 slots covered.

```



```shell
## 后面可试下不用ruby
redis-cli --cluster create replicas 1 127.0.0.1:6379 127.0.0.1:6380 127.0.0.1:6381 127.0.0.1:7379 127.0.0.1:7380 127.0.0.1:7381 --cluster-replicas 1 -a 密码
```



#### 1..4.5 查看状态

可以看到 分为：3主3从

主：6379  从：7379

主：6380  从：7380

主：6381  从：7381

```shell
[015660] 24 Jul 19:10:04.898 # Server initialized
[015660] 24 Jul 19:10:04.913 * Creating AOF base file appendonly.aof.1.base.rdb on server start
[015660] 24 Jul 19:10:04.913 * Creating AOF incr file appendonly.aof.1.incr.aof on server start
[015660] 24 Jul 19:10:04.913 * Ready to accept connections
[015660] 24 Jul 19:26:57.707 # configEpoch set to 1 via CLUSTER SET-CONFIG-EPOCH
[015660] 24 Jul 19:26:57.879 # IP address for this node updated to 127.0.0.1
[015660] 24 Jul 19:27:02.400 * Replica 127.0.0.1:7379 asks for synchronization
[015660] 24 Jul 19:27:02.400 * Partial resynchronization not accepted: Replication ID mismatch (Replica asked for 'a1c65765caf8b9f5a9c7fb9f12525021a05b57ea', my replication IDs are 'f48b0404a608c08631fe985657494bf91575228c' and '0000000000000000000000000000000000000000')
[015660] 24 Jul 19:27:02.400 * Replication backlog created, my new replication IDs are '63a7df8d6030302d46fcfeebe2634f75bce9fac6' and '0000000000000000000000000000000000000000'
[015660] 24 Jul 19:27:02.400 * Delay next BGSAVE for diskless SYNC
[015660] 24 Jul 19:27:02.494 # Cluster state changed: ok
[015660] 24 Jul 19:27:07.314 * Starting BGSAVE for SYNC with target: replicas sockets
[015660] 24 Jul 19:27:07.329 * Background RDB transfer started by pid 5032
[015660] 24 Jul 19:27:07.517 # fork operation complete
[015660] 24 Jul 19:27:07.548 * Background RDB transfer terminated with success
[015660] 24 Jul 19:27:07.548 * Streamed RDB transfer with replica 127.0.0.1:7379 succeeded (socket). Waiting for REPLCONF ACK from slave to enable streaming
[015660] 24 Jul 19:27:07.548 * Synchronization with replica 127.0.0.1:7379 succeeded

[017784] 24 Jul 19:10:47.107 # Server initialized
[017784] 24 Jul 19:10:47.123 * Creating AOF base file appendonly.aof.1.base.rdb on server start
[017784] 24 Jul 19:10:47.123 * Creating AOF incr file appendonly.aof.1.incr.aof on server start
[017784] 24 Jul 19:10:47.123 * Ready to accept connections
[017784] 24 Jul 19:26:57.785 # configEpoch set to 4 via CLUSTER SET-CONFIG-EPOCH
[017784] 24 Jul 19:26:57.926 # IP address for this node updated to 127.0.0.1
[017784] 24 Jul 19:27:02.291 * Before turning into a replica, using my own master parameters to synthesize a cached master: I may be able to synchronize with the new master with just a partial transfer.
[017784] 24 Jul 19:27:02.291 * Connecting to MASTER 127.0.0.1:6379
[017784] 24 Jul 19:27:02.291 * MASTER <-> REPLICA sync started
[017784] 24 Jul 19:27:02.291 # Cluster state changed: ok
[017784] 24 Jul 19:27:02.291 * Non blocking connect for SYNC fired the event.
[017784] 24 Jul 19:27:02.291 * Master replied to PING, replication can continue...
[017784] 24 Jul 19:27:02.385 * Trying a partial resynchronization (request a1c65765caf8b9f5a9c7fb9f12525021a05b57ea:1).
[017784] 24 Jul 19:27:07.314 * Full resync from master: 63a7df8d6030302d46fcfeebe2634f75bce9fac6:0
[017784] 24 Jul 19:27:07.517 * MASTER <-> REPLICA sync: receiving streamed RDB from master with EOF to disk
[017784] 24 Jul 19:27:07.517 * Discarding previously cached master state.
[017784] 24 Jul 19:27:07.517 * MASTER <-> REPLICA sync: Flushing old data
[017784] 24 Jul 19:27:07.517 * MASTER <-> REPLICA sync: Loading DB in memory
[017784] 24 Jul 19:27:07.517 * Loading RDB produced by version 7.0.4
[017784] 24 Jul 19:27:07.517 * RDB age 0 seconds
[017784] 24 Jul 19:27:07.517 * RDB memory usage when created 0.00 Mb
[017784] 24 Jul 19:27:07.517 # RDB file was saved with checksum disabled: no check performed.
[017784] 24 Jul 19:27:07.517 * Done loading RDB, keys loaded: 0, keys expired: 0.
[017784] 24 Jul 19:27:07.517 * MASTER <-> REPLICA sync: Finished with success
[017784] 24 Jul 19:27:07.517 * Creating AOF incr file temp-appendonly.aof.incr on background rewrite
[017784] 24 Jul 19:27:07.517 * Background append only file rewriting started by pid 18924
[017784] 24 Jul 19:27:07.735 # fork operation complete
[017784] 24 Jul 19:27:07.735 * Background AOF rewrite terminated with success
[017784] 24 Jul 19:27:07.751 * Successfully renamed the temporary AOF base file temp-rewriteaof-bg-17784.aof into appendonly.aof.2.base.rdb
[017784] 24 Jul 19:27:07.751 * Successfully renamed the temporary AOF incr file temp-appendonly.aof.incr into appendonly.aof.2.incr.aof
[017784] 24 Jul 19:27:07.751 * Removing the history file appendonly.aof.1.incr.aof in the background
[017784] 24 Jul 19:27:07.751 * Removing the history file appendonly.aof.1.base.rdb in the background
[017784] 24 Jul 19:27:07.751 * Background AOF rewrite finished successfully
---------------------------------------------------------------------------------------------------------

[006788] 24 Jul 19:10:28.572 # Server initialized
[006788] 24 Jul 19:10:28.572 * Creating AOF base file appendonly.aof.1.base.rdb on server start
[006788] 24 Jul 19:10:28.572 * Creating AOF incr file appendonly.aof.1.incr.aof on server start
[006788] 24 Jul 19:10:28.585 * Ready to accept connections
[006788] 24 Jul 19:26:57.723 # configEpoch set to 2 via CLUSTER SET-CONFIG-EPOCH
[006788] 24 Jul 19:26:57.926 # IP address for this node updated to 127.0.0.1
[006788] 24 Jul 19:27:02.400 * Replica 127.0.0.1:7380 asks for synchronization
[006788] 24 Jul 19:27:02.400 * Partial resynchronization not accepted: Replication ID mismatch (Replica asked for '21f27b627d77936ce686204936375612f1912841', my replication IDs are 'a4ea34ba0e7084eba3521179a196d397c0563461' and '0000000000000000000000000000000000000000')
[006788] 24 Jul 19:27:02.400 * Replication backlog created, my new replication IDs are 'b9bb82d816b591c45945d6e93ce054132b962506' and '0000000000000000000000000000000000000000'
[006788] 24 Jul 19:27:02.400 * Delay next BGSAVE for diskless SYNC
[006788] 24 Jul 19:27:02.494 # Cluster state changed: ok
[006788] 24 Jul 19:27:07.891 * Starting BGSAVE for SYNC with target: replicas sockets
[006788] 24 Jul 19:27:07.891 * Background RDB transfer started by pid 17588
[006788] 24 Jul 19:27:08.016 # fork operation complete
[006788] 24 Jul 19:27:08.032 * Background RDB transfer terminated with success
[006788] 24 Jul 19:27:08.032 * Streamed RDB transfer with replica 127.0.0.1:7380 succeeded (socket). Waiting for REPLCONF ACK from slave to enable streaming
[006788] 24 Jul 19:27:08.032 * Synchronization with replica 127.0.0.1:7380 succeeded

[002140] 24 Jul 19:10:51.864 # Server initialized
[002140] 24 Jul 19:10:51.896 * Creating AOF base file appendonly.aof.1.base.rdb on server start
[002140] 24 Jul 19:10:51.911 * Creating AOF incr file appendonly.aof.1.incr.aof on server start
[002140] 24 Jul 19:10:51.911 * Ready to accept connections
[002140] 24 Jul 19:26:57.801 # configEpoch set to 5 via CLUSTER SET-CONFIG-EPOCH
[002140] 24 Jul 19:26:57.926 # IP address for this node updated to 127.0.0.1
[002140] 24 Jul 19:27:02.307 * Before turning into a replica, using my own master parameters to synthesize a cached master: I may be able to synchronize with the new master with just a partial transfer.
[002140] 24 Jul 19:27:02.307 * Connecting to MASTER 127.0.0.1:6380
[002140] 24 Jul 19:27:02.307 * MASTER <-> REPLICA sync started
[002140] 24 Jul 19:27:02.307 # Cluster state changed: ok
[002140] 24 Jul 19:27:02.385 * Non blocking connect for SYNC fired the event.
[002140] 24 Jul 19:27:02.400 * Master replied to PING, replication can continue...
[002140] 24 Jul 19:27:02.400 * Trying a partial resynchronization (request 21f27b627d77936ce686204936375612f1912841:1).
[002140] 24 Jul 19:27:07.891 * Full resync from master: b9bb82d816b591c45945d6e93ce054132b962506:0
[002140] 24 Jul 19:27:08.000 * MASTER <-> REPLICA sync: receiving streamed RDB from master with EOF to disk
[002140] 24 Jul 19:27:08.000 * Discarding previously cached master state.
[002140] 24 Jul 19:27:08.000 * MASTER <-> REPLICA sync: Flushing old data
[002140] 24 Jul 19:27:08.000 * MASTER <-> REPLICA sync: Loading DB in memory
[002140] 24 Jul 19:27:08.000 * Loading RDB produced by version 7.0.4
[002140] 24 Jul 19:27:08.000 * RDB age 1 seconds
[002140] 24 Jul 19:27:08.000 * RDB memory usage when created 0.00 Mb
[002140] 24 Jul 19:27:08.000 # RDB file was saved with checksum disabled: no check performed.
[002140] 24 Jul 19:27:08.000 * Done loading RDB, keys loaded: 0, keys expired: 0.
[002140] 24 Jul 19:27:08.000 * MASTER <-> REPLICA sync: Finished with success
[002140] 24 Jul 19:27:08.000 * Creating AOF incr file temp-appendonly.aof.incr on background rewrite
[002140] 24 Jul 19:27:08.016 * Background append only file rewriting started by pid 17284
[002140] 24 Jul 19:27:08.235 # fork operation complete
[002140] 24 Jul 19:27:08.236 * Background AOF rewrite terminated with success
[002140] 24 Jul 19:27:08.251 * Successfully renamed the temporary AOF base file temp-rewriteaof-bg-2140.aof into appendonly.aof.2.base.rdb
[002140] 24 Jul 19:27:08.251 * Successfully renamed the temporary AOF incr file temp-appendonly.aof.incr into appendonly.aof.2.incr.aof
[002140] 24 Jul 19:27:08.251 * Removing the history file appendonly.aof.1.incr.aof in the background
[002140] 24 Jul 19:27:08.251 * Removing the history file appendonly.aof.1.base.rdb in the background
[002140] 24 Jul 19:27:08.283 * Background AOF rewrite finished successfully
-------------------------------------------------------------------------------------------------------
[011276] 24 Jul 19:10:40.251 # Server initialized
[011276] 24 Jul 19:10:40.251 * Creating AOF base file appendonly.aof.1.base.rdb on server start
[011276] 24 Jul 19:10:40.251 * Creating AOF incr file appendonly.aof.1.incr.aof on server start
[011276] 24 Jul 19:10:40.251 * Ready to accept connections
[011276] 24 Jul 19:26:57.769 # configEpoch set to 3 via CLUSTER SET-CONFIG-EPOCH
[011276] 24 Jul 19:26:58.036 # IP address for this node updated to 127.0.0.1
[011276] 24 Jul 19:27:02.400 * Replica 127.0.0.1:7381 asks for synchronization
[011276] 24 Jul 19:27:02.400 * Partial resynchronization not accepted: Replication ID mismatch (Replica asked for '2ee1e154a04ffccfcf59ba309061b592d9bd2ba9', my replication IDs are '3b5127be262499560e98986925ae8acba9c7dfae' and '0000000000000000000000000000000000000000')
[011276] 24 Jul 19:27:02.400 * Replication backlog created, my new replication IDs are 'efcf60f8cecba55cf136d7eadef4663968250183' and '0000000000000000000000000000000000000000'
[011276] 24 Jul 19:27:02.400 * Delay next BGSAVE for diskless SYNC
[011276] 24 Jul 19:27:02.651 # Cluster state changed: ok
[011276] 24 Jul 19:27:07.470 * Starting BGSAVE for SYNC with target: replicas sockets
[011276] 24 Jul 19:27:07.470 * Background RDB transfer started by pid 17864
[011276] 24 Jul 19:27:07.579 # fork operation complete
[011276] 24 Jul 19:27:07.595 * Background RDB transfer terminated with success
[011276] 24 Jul 19:27:07.595 * Streamed RDB transfer with replica 127.0.0.1:7381 succeeded (socket). Waiting for REPLCONF ACK from slave to enable streaming
[011276] 24 Jul 19:27:07.595 * Synchronization with replica 127.0.0.1:7381 succeeded

[018064] 24 Jul 19:10:58.652 # Server initialized
[018064] 24 Jul 19:10:58.652 * Creating AOF base file appendonly.aof.1.base.rdb on server start
[018064] 24 Jul 19:10:58.652 * Creating AOF incr file appendonly.aof.1.incr.aof on server start
[018064] 24 Jul 19:10:58.652 * Ready to accept connections
[018064] 24 Jul 19:26:57.816 # configEpoch set to 6 via CLUSTER SET-CONFIG-EPOCH
[018064] 24 Jul 19:26:58.036 # IP address for this node updated to 127.0.0.1
[018064] 24 Jul 19:27:02.400 * Before turning into a replica, using my own master parameters to synthesize a cached master: I may be able to synchronize with the new master with just a partial transfer.
[018064] 24 Jul 19:27:02.400 * Connecting to MASTER 127.0.0.1:6381
[018064] 24 Jul 19:27:02.400 * MASTER <-> REPLICA sync started
[018064] 24 Jul 19:27:02.400 # Cluster state changed: ok
[018064] 24 Jul 19:27:02.400 * Non blocking connect for SYNC fired the event.
[018064] 24 Jul 19:27:02.400 * Master replied to PING, replication can continue...
[018064] 24 Jul 19:27:02.400 * Trying a partial resynchronization (request 2ee1e154a04ffccfcf59ba309061b592d9bd2ba9:1).
[018064] 24 Jul 19:27:07.470 * Full resync from master: efcf60f8cecba55cf136d7eadef4663968250183:14
[018064] 24 Jul 19:27:07.564 * MASTER <-> REPLICA sync: receiving streamed RDB from master with EOF to disk
[018064] 24 Jul 19:27:07.579 * Discarding previously cached master state.
[018064] 24 Jul 19:27:07.579 * MASTER <-> REPLICA sync: Flushing old data
[018064] 24 Jul 19:27:07.579 * MASTER <-> REPLICA sync: Loading DB in memory
[018064] 24 Jul 19:27:07.595 * Loading RDB produced by version 7.0.4
[018064] 24 Jul 19:27:07.595 * RDB age 0 seconds
[018064] 24 Jul 19:27:07.595 * RDB memory usage when created 0.00 Mb
[018064] 24 Jul 19:27:07.595 # RDB file was saved with checksum disabled: no check performed.
[018064] 24 Jul 19:27:07.595 * Done loading RDB, keys loaded: 0, keys expired: 0.
[018064] 24 Jul 19:27:07.595 * MASTER <-> REPLICA sync: Finished with success
[018064] 24 Jul 19:27:07.626 * Creating AOF incr file temp-appendonly.aof.incr on background rewrite
[018064] 24 Jul 19:27:07.642 * Background append only file rewriting started by pid 18052
[018064] 24 Jul 19:27:07.861 # fork operation complete
[018064] 24 Jul 19:27:07.875 * Background AOF rewrite terminated with success
[018064] 24 Jul 19:27:07.875 * Successfully renamed the temporary AOF base file temp-rewriteaof-bg-18064.aof into appendonly.aof.2.base.rdb
[018064] 24 Jul 19:27:07.875 * Successfully renamed the temporary AOF incr file temp-appendonly.aof.incr into appendonly.aof.2.incr.aof
[018064] 24 Jul 19:27:07.875 * Removing the history file appendonly.aof.1.incr.aof in the background
[018064] 24 Jul 19:27:07.875 * Removing the history file appendonly.aof.1.base.rdb in the background
[018064] 24 Jul 19:27:07.875 * Background AOF rewrite finished successfully
```



```shell
# Replication
role:master
connected_slaves:1
slave0:ip=127.0.0.1,port=7379,state=online,offset=294,lag=1
master_failover_state:no-failover
master_replid:63a7df8d6030302d46fcfeebe2634f75bce9fac6
master_replid2:0000000000000000000000000000000000000000
master_repl_offset:294
second_repl_offset:-1
repl_backlog_active:1
repl_backlog_size:1048576
repl_backlog_first_byte_offset:1
repl_backlog_histlen:294

# CPU
used_cpu_sys:0.187500
used_cpu_user:0.125000
used_cpu_sys_children:0.000000
used_cpu_user_children:0.000000

# Modules

# Errorstats
errorstat_NOAUTH:count=6
errorstat_WRONGPASS:count=1

# Cluster
cluster_enabled:1

# Keyspace
127.0.0.1:6379>
```

```shell
# Replication
role:slave
master_host:127.0.0.1
master_port:6379
master_link_status:up
master_last_io_seconds_ago:10
master_sync_in_progress:0
slave_read_repl_offset:406
slave_repl_offset:406
slave_priority:100
slave_read_only:1
replica_announced:1
connected_slaves:0
master_failover_state:no-failover
master_replid:63a7df8d6030302d46fcfeebe2634f75bce9fac6
master_replid2:0000000000000000000000000000000000000000
master_repl_offset:406
second_repl_offset:-1
repl_backlog_active:1
repl_backlog_size:1048576
repl_backlog_first_byte_offset:1
repl_backlog_histlen:406

# CPU
used_cpu_sys:0.078125
used_cpu_user:0.078125
used_cpu_sys_children:0.000000
used_cpu_user_children:0.000000

# Modules

# Errorstats
errorstat_ERR:count=1
errorstat_NOAUTH:count=2

# Cluster
cluster_enabled:1

# Keyspace
127.0.0.1:7379>
```



```shell
# Replication
role:master
connected_slaves:1
slave0:ip=127.0.0.1,port=7380,state=online,offset=490,lag=1
master_failover_state:no-failover
master_replid:b9bb82d816b591c45945d6e93ce054132b962506
master_replid2:0000000000000000000000000000000000000000
master_repl_offset:490
second_repl_offset:-1
repl_backlog_active:1
repl_backlog_size:1048576
repl_backlog_first_byte_offset:1
repl_backlog_histlen:490

# CPU
used_cpu_sys:0.125000
used_cpu_user:0.109375
used_cpu_sys_children:0.000000
used_cpu_user_children:0.000000

# Modules

# Errorstats
errorstat_NOAUTH:count=3

# Cluster
cluster_enabled:1

# Keyspace
127.0.0.1:6380>
```



```shell
# Replication
role:slave
master_host:127.0.0.1
master_port:6380
master_link_status:up
master_last_io_seconds_ago:1
master_sync_in_progress:0
slave_read_repl_offset:560
slave_repl_offset:560
slave_priority:100
slave_read_only:1
replica_announced:1
connected_slaves:0
master_failover_state:no-failover
master_replid:b9bb82d816b591c45945d6e93ce054132b962506
master_replid2:0000000000000000000000000000000000000000
master_repl_offset:560
second_repl_offset:-1
repl_backlog_active:1
repl_backlog_size:1048576
repl_backlog_first_byte_offset:1
repl_backlog_histlen:560

# CPU
used_cpu_sys:0.109375
used_cpu_user:0.109375
used_cpu_sys_children:0.000000
used_cpu_user_children:0.000000

# Modules

# Errorstats
errorstat_ERR:count=1
errorstat_NOAUTH:count=2

# Cluster
cluster_enabled:1

# Keyspace
127.0.0.1:7380>
```



#### 1.4.6 测试命令

在 从：7379 设置key

```shell
127.0.0.1:7379> set aa 111
(error) MOVED 1180 127.0.0.1:6379
127.0.0.1:7379> keys *
(empty array)
127.0.0.1:7379>
```

在 主：6379 设置key,查看key，取值

```shell
127.0.0.1:6379> keys *
(empty array)
127.0.0.1:6379> set aa 11
OK
127.0.0.1:6379> keys *
1) "aa"
127.0.0.1:6379> get aa
"11"
127.0.0.1:6379>
```

在 从：7379  查看key, 取值

```shell
127.0.0.1:7379> keys *
1) "aa"
127.0.0.1:7379> get aa
(error) MOVED 1180 127.0.0.1:6379
127.0.0.1:7379>
```

与 普通的主从、sentinel表现还是不一样的【从节点】


