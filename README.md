# DarkRevenger

DarkRevenger는 Unity로 제작된 2D 액션 플랫폼 게임 프로젝트입니다.
어둡고 몰입감 있는 분위기 속에서 플레이어가 여러 구역을 탐험하며 적과 보스를 물리치고, 스토리 기반의 챕터를 진행하는 구조로 구성되어 있습니다.

## 프로젝트 개요
- Unity 2022.3.23f1 기반으로 제작된 2D 액션 게임
- 챕터형 진행 구조를 가진 플랫폼러
- 플레이어 이동, 점프, 대시, 공격 콤보, 보스전 등 다양한 게임플레이 요소 포함
- 일시정지, 옵션, 키 설정, 씬 전환 기능 구현

## 주요 기능
- 좌우 이동, 점프, 더블 점프, 대시
- 공격 콤보 시스템
- 적 AI 및 보스 AI 구현
- 체력/스탯 기반 전투 시스템
- 옵션 메뉴 및 키 재설정 기능
- 챕터 1~3 기반의 씬 진행

## 조작 방법
- 이동: 방향키 또는 A/D
- 점프: Z
- 공격: X
- 대시: Left Shift
- 일시정지: Esc

## 실행 방법
GitHub Releases에 업로드된 빌드 파일을 이용해 실행할 수 있습니다.

1. GitHub 저장소의 Releases 페이지로 이동합니다.
2. 최신 릴리스 중 "1.0v" 또는 해당 버전의 Assets에서 빌드 파일을 다운로드합니다.
3. 압축을 해제한 뒤 실행 파일(.exe)을 더블클릭하면 게임을 실행할 수 있습니다.
4. 필요하다면 빌드 파일이 포함된 폴더 전체를 함께 사용하면 됩니다.

> Unity 에디터로 직접 실행하고 싶다면, Unity Hub에서 Unity 2022.3.23f1을 설치한 뒤 [DarkRevenger](DarkRevenger)를 열고 Play 버튼을 눌러 실행할 수 있습니다.

## 폴더 구조
- [DarkRevenger/Assets](DarkRevenger/Assets): 게임 리소스, 씬, 스크립트
- [DarkRevenger/ProjectSettings](DarkRevenger/ProjectSettings): Unity 프로젝트 설정

## 참고
이 프로젝트는 Unity 2D 게임 개발 학습 및 포트폴리오용으로 구성된 작품입니다.


# 🎮 Dark Revenger (다크 리벤저)

![unity](https://img.shields.io/badge/unity-2022.3.23f1-black?style=for-the-badge&logo=unity&logoColor=white)
![csharp](https://img.shields.io/badge/c%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![platform](https://img.shields.io/badge/platform-PC%20%2F%20Windows-blue?style=for-the-badge)

[cite_start]※ 본 프로젝트는 Unity 엔진 기반으로 제작된 2D 액션 플랫폼 게임 프로젝트입니다. [cite: 3]  
어둡고 몰입감 있는 분위기 속에서 플레이어가 여러 구역을 탐험하며 적과 보스를 물리치고, 스토리 기반의 챕터를 진행하는 구조로 구성되어 있습니다.

---

## 📌 프로젝트 소개

### 프로젝트 개요
* [cite_start]**스토리**: 가족을 잃은 주인공이 분노와 복수심으로 적들에게 향하는 여정입니다[cite: 4].
* [cite_start]**장르/시점**: 2D 횡스크롤 액션 플랫포머 게임입니다[cite: 3].
* [cite_start]**핵심 그래픽**: 암울한 배경과 정교한 도트 그래픽이 특징입니다[cite: 3, 4].
* [cite_start]**클리어 조건**: 게임 마지막 챕터의 최종 보스를 처치하면 클리어됩니다[cite: 4].

### 주요 기능
* [cite_start]**플레이어 액션**: 좌우 이동, 점프, 더블 점프, 무적 프레임을 이용한 대시 및 공격 콤보 시스템을 제공합니다[cite: 11].
* [cite_start]**전투 및 AI 시스템**: 체력/스탯 기반 전투 시스템 및 고유한 패턴을 가진 적 AI와 보스 AI를 구현했습니다[cite: 13, 14].
* [cite_start]**보스 처치 보상**: 각 지역의 보스를 격파하면 캐릭터의 체력 `+50`, 공격력 `+10`이 영구적으로 강화됩니다[cite: 16].
* [cite_start]**체크포인트 시스템**: 사망 시 별도의 저장 없이 해당 지역 시작점이나 보스전 직전의 체크포인트로 돌아갑니다[cite: 15].
* **UI 및 편의 기능**: 일시정지, 옵션 메뉴, 사용자 편의를 위한 키 재설정 기능을 지원합니다.

<p align="center">
  <img src="images/main_title.png" alt="Dark Revenger Main Title" width="600">
</p>

---

## 🕹️ 메뉴 구성 및 챕터 안내

### 🗺️ 챕터별 디자인 구성
[cite_start]게임은 보스가 존재하는 총 3개의 지역을 기반으로 움직이며, 각 지역의 몬스터는 테마에 맞는 독창적인 컨셉과 패턴을 가집니다[cite: 13, 14].

| [cite_start]챕터 1 (구동현 담당 [cite: 11]) | [cite_start]챕터 2 (박민준 담당 [cite: 11]) | [cite_start]챕터 3 (전승재 담당 [cite: 11]) |
| :---: | :---: | :---: |
| <img src="images/chapter1.png" width="200"> | <img src="images/chapter2.png" width="200"> | <img src="images/chapter3.png" width="200"> |
| 숲 및 동굴 테마 디자인 | 성 및 감옥 내부 UI 디자인 | 최종 보스 방 및 플레이어 액션 |

---

## 🎮 조작 방법 (Controls)

| 행동 | 단축키 | 비고 |
| :--- | :--- | :--- |
| **이동** | 방향키 또는 `A` / `D` | 좌우 이동 |
| **점프** | `Z` | 더블 점프 가능 |
| **공격** | `X` | 타이밍에 따른 콤보 연계 |
| **대시** | `Left Shift` | [cite_start]무적 프레임을 이용한 패턴 회피 [cite: 11] |
| **일시정지** | `Esc` | 옵션 및 키 설정 진입 가능 |

---

## 📁 폴더 구조 (Folder Structure)

```🗂️
DarkRevenger/
├── DarkRevenger/
│   ├── Assets/              # 게임 리소스, 씬(Scene 1~3), C# 스크립트 코드
│   └── ProjectSettings/     # Unity 프로젝트 환경 설정 파일
└── README.md
