# GensokyoSurvivor - Core C# Scripts

本リポジトリは、Unity 2D アクションサバイバーゲーム『幻想郷サバイバー』のコアロジックを管理する C# ソースコード集です。

## 🛠️ 主な実装機能とスクリプト構成

### 1. 状態管理 & コアロジック
* **`CharacterBase.cs`** : プレイヤーおよび敵の基本ステータス管理
* **`Enemy.cs`** / **`EnemySpawner.cs`** : 敵の移動AI、FSM状態推移、生成スポナー logic
* **`BulletBase.cs`** : 弾幕・攻撃判定の物理・判定処理

### 2. スキル & ビルドシステム
* **`Skillbase.cs`** / **`SkillAcquisition.cs`** : スキル発動ロジックと取得処理
* **`SkillStatModifier.cs`** / **`PlayerStatModifier.cs`** : レベルアップ時のステータス補正
* **`UpgradeOptionBase.cs`** : 3選1ランダムビルド選択処理

### 3. システム & UI
* **`SaveSystem.cs`** : ゲームデータセーブ / ロード処理
* **`AudioManager.cs`** : BGM / SFX の再生管理
* **`BattleUI.cs`** / **`SkillSelectionUI.cs`** / **`TitleScreen.cs`** : 戦闘画面およびUI制御

---

## 🎮 動作環境 / 技術スタック
* **Engine**: Unity (URP 2D)
* **Language**: C#
* **Architecture**: FSM (Finite State Machine), Object Pooling

## 📝 開発コードに関する補足 (Development Notes)

* **コード内コメント・一部変数について：**
  個人開発における実装スピード向上のため、スクリプト内部（一部のプライベート変数・UI参照名およびコメント）には中国語表記が含まれています。
* **命名規則の統一：**
  主要なクラス名（Class Name）、パブリックメソッド（Public Method）、およびファイル名については、上記の通り C# 標準の英字命名（PascalCase）に統一・整理して管理しています。
