# Simulation and Modeling Project

## Overview

This repository contains four C# applications developed for the **Simulation & Modeling Course (2024–2025)** at the **Faculty of Computer and Information Sciences, Ain Shams University**. The project simulates real-world systems such as queueing, inventory control, and random number generation, utilizing **object-oriented programming (OOP)**, **GUI design**, and **probabilistic modeling**.

Each task demonstrates robust simulation logic, performance evaluation, and user-friendly interfaces.

---

## Project Structure

### 1. Multi-Channel Queue Simulation

* Simulates a flexible multi-server queue system with stochastic inter-arrival and service times.
* Supports server selection methods: **Highest Priority**, **Random**, and **Least Utilization**.
* Outputs a simulation table for 100 customers and calculates metrics such as **average waiting time** and **server utilization**.
* Visualizes server activity through charts and includes a GUI for user input and results.
* 📄 [View Detailed Presentation](https://docs.google.com/presentation/d/1hkAIZTmPaCg_77f1CXxtu_TD0R8sslbb/edit?usp=drive_link&ouid=114294364593949962074&rtpof=true&sd=true)

### 2. Newspaper Seller's Inventory Problem

* Optimizes daily newspaper purchases over 20 days using probabilistic demand distributions for **Good**, **Fair**, and **Poor** newsdays.
* Calculates **profits**, **costs**, **lost profits**, and **scrap value**.
* Includes a simulation table, performance metrics, and a GUI for input and validation.
* 📄 [View Detailed Presentation](https://docs.google.com/presentation/d/10J15KxHyCcLFxBsF8ptdcnHJwI38R_hw/edit?usp=drive_link&ouid=114294364593949962074&rtpof=true&sd=true)

### 3. Refrigerator Inventory System

* Models a periodic-review inventory policy for refrigerators with parameters: **review period (N)** and **order-up-to level (M)**.
* Simulates 25 days of operations, tracking inventory levels, **shortages (backorders)**, and **orders**.
* Produces a simulation table and metrics such as **average ending inventory** and **number of shortages**, with an interactive GUI.
* 📄 [View Detailed Presentation](https://docs.google.com/presentation/d/10k2fOFkNVPrt1MK4cmUWjTYazrO9ZGDT/edit?usp=drive_link&ouid=114294364593949962074&rtpof=true&sd=true)

### 4. Linear Congruential Generator (LCG)

* Implements a pseudorandom number generator using the formula:
  `Xₙ₊₁ = (a * Xₙ + c) mod m`
* Generates both **integer** and **real** random numbers.
* Calculates the **cycle length** and supports commonly used parameter sets.
* Displays output using a DataGridView GUI.
* 📄 [View Detailed Presentation](https://docs.google.com/presentation/d/1LCAvDPevPU2-IeY-s3V1pasFPvPWrB5l/edit?usp=drive_link&ouid=114294364593949962074&rtpof=true&sd=true)

---

## Technologies

* **Language**: C#
* **Framework**: .NET Framework 4.5.1
* **GUI**: Windows Forms
* **Libraries**: `System`, `System.Windows.Forms`, `System.Collections.Generic`
* **IDE**: Visual Studio


