using UnityEngine;

namespace CLEARSKIES
{
        public static class Layer
        {
            static Layer()
            {
                Layer.GroundHit = (Layer.Ground | Layer.EnemyHitBox);
                Layer.GroundAABBHit = (Layer.Ground | Layer.AABB | Layer.EnemyHitBox);
                Layer.GroundEnemyHit = (Layer.Ground | Layer.Enemy | Layer.EnemyHitBox);
                Layer.GroundPlayersHit = (Layer.Ground | Layer.Players | Layer.EnemyHitBox);
                Layer.GroundNetworkObjectHit = (Layer.Ground | Layer.NetworkObject | Layer.EnemyHitBox);
                Layer.GroundAttackHit = (Layer.Ground | Layer.Attack | Layer.EnemyHitBox);
                Layer.GroundEnemyPlayersHit = (Layer.Ground | Layer.Enemy | Layer.Players | Layer.EnemyHitBox);
                Layer.GroundEnemyAABBHit = (Layer.Ground | Layer.Enemy | Layer.AABB | Layer.EnemyHitBox);
                Layer.GroundEnemyAttackHit = (Layer.Ground | Layer.Enemy | Layer.Attack | Layer.EnemyHitBox);
                Layer.GroundEnemyNetworkObjectHit = (Layer.Ground | Layer.Enemy | Layer.NetworkObject | Layer.EnemyHitBox);
                Layer.GroundPlayersNetworkObject = (Layer.Ground | Layer.Players | Layer.NetworkObject);
                Layer.GroundPlayersNetworkObjectHit = (Layer.Ground | Layer.Players | Layer.NetworkObject | Layer.EnemyHitBox);
                Layer.GroundEnemyPlayersNetworkObjectHit = (Layer.Ground | Layer.Enemy | Layer.Players | Layer.NetworkObject | Layer.EnemyHitBox);
                Layer.GroundEnemyPlayersAttackHit = (Layer.Ground | Layer.Enemy | Layer.Players | Layer.Attack | Layer.EnemyHitBox);
                Layer.GroundEnemyPlayersAABBHit = (Layer.Ground | Layer.Enemy | Layer.Players | Layer.AABB | Layer.EnemyHitBox);
                Layer.GroundPlayersAttackHit = (Layer.Ground | Layer.Players | Layer.Attack | Layer.EnemyHitBox);
                Layer.GroundPlayersAABBHit = (Layer.Ground | Layer.Players | Layer.AABB | Layer.EnemyHitBox);
                Layer.GroundPlayersNetworkObjectAttackHit = (Layer.Ground | Layer.NetworkObject | Layer.Players | Layer.Attack | Layer.EnemyHitBox);
                Layer.GroundPlayersNetworkObjectAABBHit = (Layer.Ground | Layer.Players | Layer.Attack | Layer.AABB | Layer.EnemyHitBox);
                Layer.GroundEnemyPlayersAttackAABB = (Layer.Ground | Layer.Enemy | Layer.Players | Layer.Attack | Layer.AABB);
                Layer.GroundPlayersAttackAABBHit = (Layer.Ground | Layer.Players | Layer.Attack | Layer.AABB | Layer.EnemyHitBox);
                Layer.GroundEnemyPlayersAttackAABBHit = (Layer.Ground | Layer.Enemy | Layer.Players | Layer.Attack | Layer.AABB | Layer.EnemyHitBox);
                Layer.GroundEnemyPlayersNetworkObjectAttackHit = (Layer.Ground | Layer.Enemy | Layer.Players | Layer.NetworkObject | Layer.Attack | Layer.EnemyHitBox);
                Layer.GroundEnemyPlayersNetworkObjectAABBHit = (Layer.Ground | Layer.Enemy | Layer.Players | Layer.NetworkObject | Layer.AABB | Layer.EnemyHitBox);
                Layer.GroundEnemyPlayersNetworkObjectAttackAABBHit = (Layer.Ground | Layer.Enemy | Layer.Players | Layer.NetworkObject | Layer.Attack | Layer.AABB | Layer.EnemyHitBox);
                Layer.EnemyHit = (Layer.Enemy | Layer.EnemyHitBox);
                Layer.EnemyAttackHit = (Layer.Enemy | Layer.Attack | Layer.EnemyHitBox);
                Layer.EnemyPlayersHit = (Layer.Enemy | Layer.Players | Layer.EnemyHitBox);
                Layer.EnemyAABBHit = (Layer.Enemy | Layer.AABB | Layer.EnemyHitBox);
                Layer.EnemyNetworkObjectHit = (Layer.Enemy | Layer.NetworkObject | Layer.EnemyHitBox);
                Layer.EnemyPlayersAABBHit = (Layer.Enemy | Layer.Players | Layer.AABB | Layer.EnemyHitBox);
                Layer.EnemyPlayersAttackHit = (Layer.Enemy | Layer.Players | Layer.Attack | Layer.EnemyHitBox);
                Layer.EnemyPlayersNetworkObjectAttackHit = (Layer.Enemy | Layer.Players | Layer.NetworkObject | Layer.Attack | Layer.EnemyHitBox);
                Layer.EnemyPlayersNetworkObjectAABBHit = (Layer.Enemy | Layer.Players | Layer.NetworkObject | Layer.AABB | Layer.EnemyHitBox);
                Layer.EnemyPlayersNetworkObjectAttackAABBHit = (Layer.Enemy | Layer.Players | Layer.NetworkObject | Layer.Attack | Layer.AABB | Layer.EnemyHitBox);
                Layer.PlayersHit = (Layer.Players | Layer.EnemyHitBox);
                Layer.PlayersAttackHit = (Layer.Players | Layer.Attack | Layer.EnemyHitBox);
                Layer.PlayersAABBHit = (Layer.Players | Layer.AABB | Layer.EnemyHitBox);
                Layer.PlayersAttackAABBHit = (Layer.Players | Layer.Attack | Layer.AABB | Layer.EnemyHitBox);
                Layer.PlayersNetworkObjectHit = (Layer.Players | Layer.NetworkObject | Layer.EnemyHitBox);
                Layer.PlayersNetworkObjectAABBHit = (Layer.Players | Layer.NetworkObject | Layer.AABB | Layer.EnemyHitBox);
                Layer.PlayersNetworkObjectAttackHit = (Layer.Players | Layer.NetworkObject | Layer.Attack | Layer.EnemyHitBox);
                Layer.PlayersNetworkObjectAttackAABBHit = (Layer.Players | Layer.NetworkObject | Layer.Attack | Layer.AABB | Layer.EnemyHitBox);
                Layer.NetworkObjectHit = (Layer.NetworkObject | Layer.EnemyHitBox);
                Layer.NetworkObjectAABBHit = (Layer.NetworkObject | Layer.AABB | Layer.EnemyHitBox);
                Layer.NetworkObjectAttackHit = (Layer.NetworkObject | Layer.Attack | Layer.EnemyHitBox);
                Layer.NetworkObjectAttackAABBHit = (Layer.NetworkObject | Layer.Attack | Layer.AABB | Layer.EnemyHitBox);
                Layer.AttackHit = (Layer.Attack | Layer.EnemyHitBox);
                Layer.AABBHit = (Layer.AABB | Layer.EnemyHitBox);
                Layer.AttackAABBHit = (Layer.Attack | Layer.AABB | Layer.EnemyHitBox);
                Layer.GroundEnemy = (Layer.Ground | Layer.Enemy);
                Layer.GroundPlayers = (Layer.Ground | Layer.Players);
                Layer.GroundAttack = (Layer.Ground | Layer.Attack);
                Layer.GroundAABB = (Layer.Ground | Layer.AABB);
                Layer.GroundPlayersNetworkObjectAABB = (Layer.Ground | Layer.Players | Layer.NetworkObject | Layer.AABB);
                Layer.GroundEnemyPlayersNetworkObjectAABB = (Layer.Ground | Layer.Enemy | Layer.Players | Layer.NetworkObject | Layer.AABB);
                Layer.GroundPlayersAttackAABB = (Layer.Ground | Layer.Players | Layer.Attack | Layer.AABB);
                Layer.GroundPlayersAABB = (Layer.Ground | Layer.Players | Layer.AABB);
                Layer.GroundEnemyAABB = (Layer.Ground | Layer.Enemy | Layer.AABB);
                Layer.GroundPlayersAttack = (Layer.Ground | Layer.Players | Layer.Attack);
                Layer.GroundEnemyAttack = (Layer.Ground | Layer.Enemy | Layer.Attack);
                Layer.GroundNetworkObject = (Layer.Ground | Layer.NetworkObject);
                Layer.GroundPlayersNetworkObjectAttack = (Layer.Ground | Layer.Players | Layer.NetworkObject);
                Layer.GroundEnemyPlayersNetworkObjectAttackAABB = (Layer.Ground | Layer.Enemy | Layer.Players | Layer.NetworkObject | Layer.Attack | Layer.AABB);
                Layer.GroundEnemyNetworkObject = (Layer.Ground | Layer.Enemy | Layer.NetworkObject);
                Layer.GroundEnemyPlayers = (Layer.Ground | Layer.Enemy | Layer.Players);
                Layer.GroundEnemyPlayersNetworkObject = (Layer.Ground | Layer.Enemy | Layer.Players | Layer.NetworkObject);
                Layer.GroundEnemyPlayersNetworkObjectAttack = (Layer.Ground | Layer.Enemy | Layer.Players | Layer.NetworkObject | Layer.Attack);
                Layer.EnemyNetworkObject = (Layer.Enemy | Layer.NetworkObject);
                Layer.EnemyPlayers = (Layer.Enemy | Layer.Players);
                Layer.EnemyAttack = (Layer.Enemy | Layer.Attack);
                Layer.EnemyAABB = (Layer.Enemy | Layer.AABB);
                Layer.EnemyAttackAABB = (Layer.Enemy | Layer.Attack | Layer.AABB);
                Layer.EnemyPlayersAttack = (Layer.Enemy | Layer.Players | Layer.Attack);
                Layer.EnemyPlayersNetworkObject = (Layer.Enemy | Layer.Players | Layer.NetworkObject);
                Layer.EnemyPlayersAABB = (Layer.Enemy | Layer.Players | Layer.AABB);
                Layer.EnemyNetworkObjectAttack = (Layer.Enemy | Layer.NetworkObject | Layer.Attack);
                Layer.EnemyPlayersNetworkObjectAttack = (Layer.Enemy | Layer.Players | Layer.NetworkObject | Layer.Attack);
                Layer.EnemyPlayersNetworkObjectAABB = (Layer.Enemy | Layer.Players | Layer.NetworkObject | Layer.AABB);
                Layer.EnemyPlayersNetworkObjectAttackAABB = (Layer.Enemy | Layer.Players | Layer.NetworkObject | Layer.Attack | Layer.AABB);
                Layer.PlayersAttack = (Layer.Players | Layer.Attack);
                Layer.PlayersAABB = (Layer.Players | Layer.AABB);
                Layer.PlayersAttackAABB = (Layer.Players | Layer.Attack | Layer.AABB);
                Layer.PlayersNetworkObject = (Layer.Players | Layer.NetworkObject);
                Layer.PlayersNetworkObjectAttack = (Layer.Players | Layer.NetworkObject | Layer.Attack);
                Layer.PlayersNetworkObjectAABB = (Layer.Players | Layer.NetworkObject | Layer.AABB);
                Layer.PlayersNetworkObjectAttackAABB = (Layer.Players | Layer.NetworkObject | Layer.Attack | Layer.AABB);
                Layer.NetworkObjectAABB = (Layer.NetworkObject | Layer.AABB);
                Layer.NetworkObjectAttack = (Layer.NetworkObject | Layer.Attack);
                Layer.NetworkObjectAttackAABB = (Layer.NetworkObject | Layer.Attack | Layer.AABB);
                Layer.AttackAABB = (Layer.Attack | Layer.AABB);
            }
            public const int DefaultN = 0;
            public const int TransparentFXN = 1;
            public const int Ignore_RaycastN = 2;
            public const int WaterN = 4;
            public const int UIN = 5;         
            public const int PlayersN = 8;       
            public const int GroundN = 9;        
            public const int EnemyN = 10;         
            public const int AABBN = 11;         
            public const int FXN = 12;        
            public const int NetworkObjectN = 13;       
            public const int InnerContactN = 14;        
            public const int noPhysicsN = 15;         
            public const int AttackN = 16;        
            public const int PlayerHitBoxN = 17; 
            public const int EnemyAttackBoxN = 18;          
            public const int EnemyHitBoxN = 19;        
            public static readonly LayerMask Default = 1;        
            public static readonly LayerMask TransparentFX = 2;
            public static readonly LayerMask Ignore_Raycast = 4;       
            public static readonly LayerMask Water = 16;    
            public static readonly LayerMask UI = 32;    
            public static readonly LayerMask FX = 4096;       
            public static readonly LayerMask InnerContact = 16384;        
            public static readonly LayerMask noPhysics = 32768;        
            public static readonly LayerMask PlayerHitBox = 131072;         
            public static readonly LayerMask EnemyAttackBox = 262144;  
            public static readonly LayerMask Ground = 512; 
            public static readonly LayerMask GroundHit;       
            public static readonly LayerMask GroundAABB;          
            public static readonly LayerMask GroundEnemy;          
            public static readonly LayerMask GroundPlayers;
            public static readonly LayerMask GroundNetworkObject;           
            public static readonly LayerMask GroundAttack;        
            public static readonly LayerMask GroundEnemyHit;     
            public static readonly LayerMask GroundAABBHit;
            public static readonly LayerMask GroundPlayersHit;  
            public static readonly LayerMask GroundEnemyAABB;
            public static readonly LayerMask GroundEnemyPlayers;
            public static readonly LayerMask GroundNetworkObjectHit;
            public static readonly LayerMask GroundEnemyAttack;  
            public static readonly LayerMask GroundEnemyAABBHit;
            public static readonly LayerMask GroundAttackHit;       
            public static readonly LayerMask GroundEnemyPlayersHit;         
            public static readonly LayerMask GroundEnemyAttackHit;
            public static readonly LayerMask GroundEnemyNetworkObject;          
            public static readonly LayerMask GroundEnemyNetworkObjectHit;
            public static readonly LayerMask GroundEnemyPlayersNetworkObject;        
            public static readonly LayerMask GroundPlayersAttack;     
            public static readonly LayerMask GroundPlayersAABB;
            public static readonly LayerMask GroundPlayersNetworkObject;          
            public static readonly LayerMask GroundPlayersNetworkObjectHit;       
            public static readonly LayerMask GroundPlayersAttackHit;      
            public static readonly LayerMask GroundEnemyPlayersNetworkObjectHit;   
            public static readonly LayerMask GroundPlayersAABBHit;
            public static readonly LayerMask GroundEnemyPlayersAttackHit;

            // Token: 0x040005F2 RID: 1522
            public static readonly LayerMask GroundEnemyPlayersAABBHit;

            // Token: 0x040005F3 RID: 1523
            public static readonly LayerMask GroundPlayersNetworkObjectAttack;

            // Token: 0x040005F4 RID: 1524
            public static readonly LayerMask GroundPlayersNetworkObjectAABB;

            // Token: 0x040005F5 RID: 1525
            public static readonly LayerMask GroundPlayersAttackAABB;

            // Token: 0x040005F6 RID: 1526
            public static readonly LayerMask GroundPlayersNetworkObjectAttackHit;

            // Token: 0x040005F7 RID: 1527
            public static readonly LayerMask GroundPlayersNetworkObjectAABBHit;

            // Token: 0x040005F8 RID: 1528
            public static readonly LayerMask GroundPlayersAttackAABBHit;

            // Token: 0x040005F9 RID: 1529
            public static readonly LayerMask GroundEnemyPlayersNetworkObjectAttack;

            // Token: 0x040005FA RID: 1530
            public static readonly LayerMask GroundEnemyPlayersNetworkObjectAABB;

            // Token: 0x040005FB RID: 1531
            public static readonly LayerMask GroundEnemyPlayersNetworkObjectAttackHit;

            // Token: 0x040005FC RID: 1532
            public static readonly LayerMask GroundEnemyPlayersNetworkObjectAABBHit;

            // Token: 0x040005FD RID: 1533
            public static readonly LayerMask GroundEnemyPlayersAttackAABB;

            // Token: 0x040005FE RID: 1534
            public static readonly LayerMask GroundEnemyPlayersAttackAABBHit;

            // Token: 0x040005FF RID: 1535
            public static readonly LayerMask GroundEnemyPlayersNetworkObjectAttackAABB;

            // Token: 0x04000600 RID: 1536
            public static readonly LayerMask GroundEnemyPlayersNetworkObjectAttackAABBHit;

            // Token: 0x04000601 RID: 1537
            public static readonly LayerMask Enemy = 1024;

            // Token: 0x04000602 RID: 1538
            public static readonly LayerMask EnemyHit;

            // Token: 0x04000603 RID: 1539
            public static readonly LayerMask EnemyAABB;

            // Token: 0x04000604 RID: 1540
            public static readonly LayerMask EnemyPlayers;

            // Token: 0x04000605 RID: 1541
            public static readonly LayerMask EnemyNetworkObject;

            // Token: 0x04000606 RID: 1542
            public static readonly LayerMask EnemyAttack;

            // Token: 0x04000607 RID: 1543
            public static readonly LayerMask EnemyAttackHit;

            // Token: 0x04000608 RID: 1544
            public static readonly LayerMask EnemyAttackAABB;

            // Token: 0x04000609 RID: 1545
            public static readonly LayerMask EnemyPlayersAABB;

            // Token: 0x0400060A RID: 1546
            public static readonly LayerMask EnemyPlayersHit;

            // Token: 0x0400060B RID: 1547
            public static readonly LayerMask EnemyPlayersNetworkObject;

            // Token: 0x0400060C RID: 1548
            public static readonly LayerMask EnemyAABBHit;

            // Token: 0x0400060D RID: 1549
            public static readonly LayerMask EnemyPlayersAttack;

            // Token: 0x0400060E RID: 1550
            public static readonly LayerMask EnemyNetworkObjectAttack;

            // Token: 0x0400060F RID: 1551
            public static readonly LayerMask EnemyPlayersNetworkObjectAttack;

            // Token: 0x04000610 RID: 1552
            public static readonly LayerMask EnemyPlayersAABBHit;

            // Token: 0x04000611 RID: 1553
            public static readonly LayerMask EnemyNetworkObjectHit;

            // Token: 0x04000612 RID: 1554
            public static readonly LayerMask EnemyPlayersAttackHit;

            // Token: 0x04000613 RID: 1555
            public static readonly LayerMask EnemyPlayersNetworkObjectAABB;

            // Token: 0x04000614 RID: 1556
            public static readonly LayerMask EnemyPlayersNetworkObjectAttackHit;

            // Token: 0x04000615 RID: 1557
            public static readonly LayerMask EnemyPlayersNetworkObjectAABBHit;

            // Token: 0x04000616 RID: 1558
            public static readonly LayerMask EnemyPlayersNetworkObjectAttackAABB;

            // Token: 0x04000617 RID: 1559
            public static readonly LayerMask EnemyPlayersNetworkObjectAttackAABBHit;

            // Token: 0x04000618 RID: 1560
            public static readonly LayerMask Players = 256;

            // Token: 0x04000619 RID: 1561
            public static readonly LayerMask PlayersHit;

            // Token: 0x0400061A RID: 1562
            public static readonly LayerMask PlayersAABB;

            // Token: 0x0400061B RID: 1563
            public static readonly LayerMask PlayersNetworkObject;

            // Token: 0x0400061C RID: 1564
            public static readonly LayerMask PlayersAttack;

            // Token: 0x0400061D RID: 1565
            public static readonly LayerMask PlayersAttackHit;

            // Token: 0x0400061E RID: 1566
            public static readonly LayerMask PlayersNetworkObjectAttack;

            // Token: 0x0400061F RID: 1567
            public static readonly LayerMask PlayersAttackAABB;

            // Token: 0x04000620 RID: 1568
            public static readonly LayerMask PlayersAttackAABBHit;

            // Token: 0x04000621 RID: 1569
            public static readonly LayerMask PlayersAABBHit;

            // Token: 0x04000622 RID: 1570
            public static readonly LayerMask PlayersNetworkObjectHit;

            // Token: 0x04000623 RID: 1571
            public static readonly LayerMask PlayersNetworkObjectAABB;

            // Token: 0x04000624 RID: 1572
            public static readonly LayerMask PlayersNetworkObjectAttackAABB;

            // Token: 0x04000625 RID: 1573
            public static readonly LayerMask PlayersNetworkObjectAABBHit;

            // Token: 0x04000626 RID: 1574
            public static readonly LayerMask PlayersNetworkObjectAttackHit;

            // Token: 0x04000627 RID: 1575
            public static readonly LayerMask PlayersNetworkObjectAttackAABBHit;

            // Token: 0x04000628 RID: 1576
            public static readonly LayerMask NetworkObject = 8192;

            // Token: 0x04000629 RID: 1577
            public static readonly LayerMask NetworkObjectHit;

            // Token: 0x0400062A RID: 1578
            public static readonly LayerMask NetworkObjectAABB;

            // Token: 0x0400062B RID: 1579
            public static readonly LayerMask NetworkObjectAttack;

            // Token: 0x0400062C RID: 1580
            public static readonly LayerMask NetworkObjectAttackAABB;

            // Token: 0x0400062D RID: 1581
            public static readonly LayerMask NetworkObjectAABBHit;

            // Token: 0x0400062E RID: 1582
            public static readonly LayerMask NetworkObjectAttackHit;

            // Token: 0x0400062F RID: 1583
            public static readonly LayerMask NetworkObjectAttackAABBHit;

            // Token: 0x04000630 RID: 1584
            public static readonly LayerMask Attack = 65536;

            // Token: 0x04000631 RID: 1585
            public static readonly LayerMask AttackAABB;

            // Token: 0x04000632 RID: 1586
            public static readonly LayerMask AttackHit;

            // Token: 0x04000633 RID: 1587
            public static readonly LayerMask AttackAABBHit;

            // Token: 0x04000634 RID: 1588
            public static readonly LayerMask AABB = 2048;

            // Token: 0x04000635 RID: 1589
            public static readonly LayerMask AABBHit;

            // Token: 0x04000636 RID: 1590
            public static readonly LayerMask EnemyHitBox = 524288;
        }
    

}
