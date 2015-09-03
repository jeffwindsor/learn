﻿#load "Collections.fs"
#load "Cellular.fs"
open Bioinformatics

let countPointMutations dna1 dna2 =
    Seq.filteri2 (fun i x y -> if x = y then None else Some(i)) dna1 dna2
    |> Seq.length

//Problem:
let dna1 = "ACCCAATGGGAAAGCACTGAAGGTAATCAAAGCTCTTCTACTTCCTGCTCCGAAAGTGACGAGCATCAGGTTGGGGATTATGGGGTCGCTCTGCAGGTCTAAGGAATGAAAAGGACACAATGCCGGGTCGTTCATCCACCTGACTGGACCCAGTTGTGCGCCCATTTCATACCACTTTGCTCTAGTATTTTGTAGCTCTGATAAAGACATTCGTTTGTGGACGGATCTCAACACAGTTATCTAGCATAGCGGCTGCGGCGGAGGGCGATTAAAGAGGGATGTATTCCATAGTATCCTGATATTCAAATAAATTCCCGGATGTAGTTTACACCCCTTAATAACCGCTGGGGTCCTCCACTGTCTGGGTTGGATTTTCGAGCTCCGTTCGGGCCTTACTGCGTCAATAGACAGACTCCGAATTTAGCCCCCTAACATGATACTGTAAGCTGGCCCTGAGGGTTTGGCAGTGACTGAGTGTATGGCTAAATTCGGTGGGACGATGCTAGGACATCTTACTCTCTAGCTTAGAGTAAACAATAAGCAGTGCTCTTCGAGAGGCTGGAAAGTATCCAGATGCGAACTCGTAGTAAGTGCAAACTAAAAACGTATTTATGCTAGTTGTCGAAAAACCTAGTAGGGCTGTGTACCATTGAATCAGAGGGCGAGCTTGTACTATGAGTCCTCTTCGCTAGGTAGCATCGCGTAGAAGACTCGCAAGGACACTTATTCTAGGCGTAGCGACTAGTCGACTGTATTTTGTTACTGTCCGTACCACCAGCATGGCTGGCAGTCAACGGTTCAATCGACTTGGTCGGGTCGCACCAACATGAAGACAGGCTCTCCACGAAACACCCATACACACTGGGGTAGCCTCTGGCTCAGAGCTCCACCTTTCTACGAGTGGTATTGTCTTGGTGGTTGATCC"
let dna2 = "TCCTCAAGGGACTCCACGGTTGCTCAGCAAAGCACGCTCTCATCCTATACCCAAAGAGACTGACAGAAGGTTGGGGCGTATAATCGGATTAATCAGCGTGTCGAAGCGGAAACTCATCACATCCGCGTCCTTTACCCAAGAAGCTCGACCCAATGGGGCGCGGCGTTCATACGACGTACCTCGCGCACATGGGTGCTAATGTAATGAAAATCGCACGTAGACGGTTGTAAACCCATTAAACTTAGTCAGCGGCGAGACCAGAGGTTAAGGGGGGAGGGCTTAAATCCCCCCAATCGTGGGATTCATCAGAATACCGTGTTCTGGATGACGCGGCTTTGCAATCTTAGGGAACGACATTTGTCTAGGCCAGATTTACCTAATACGCACAGCCCTTACTGTCCGACTTGTTCGATTCAGCAGTTTCGCGATGCAGGGGATAGAGTATGATGCGCGTATGGCTTCAGGTTAAGTGATCTGGAAACATAAAACCAGTTCGCAGACGTTGGGTTGTTTTAGAGGCTATCGCGTGTTAAGACACATGAAGTAGTCTTCGATGGGAGCGAAAGTATCAATGGCAGAACATGTAGGCAGTACTGACTGAATACGTATATAGAAGACTTCACATCATTCCTCGAAGATTACTAGTCTTTTGATTGAATGGAAGGCGTTGCAATATAGTTCCTGTTGGCTCACGAGTATTGGTTAGTATTCCGCCGAAGCATCCTCCGTTTATAACTACGAGTCGCGGCGAGTATTCTGGTATCGTGCGTAGTACGCTCCCGGATGGTAGATAAAGGCTCAACCTGCTTCCGATGCTCGCATACTCAAGAACGCTCGCGGCCCACCTCACACTGGAGCACATGAGGGTAGCAAGCCACGTTTGCAGGCAACTCGCTCGTGCTGGGAGAGGGGCGGTTGCTCTTCT"
countPointMutations dna1 dna2
|> printfn "%i"