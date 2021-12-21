namespace SortingAlgorithms
{
    public enum SortingAlgorithm
    {
        // Exchange Sorts
        #region Exchange Sorts
        BogoSort,
        BubbleSort,
        CircleSort,
        CocktailShakerSort,
        CombSort,
        ExchangeSort,
        GnomeSort,
        OddEvenSort,
        ProportionExtendSort,
        QuickSort,
        SlowSort,
        StoogeSort,
        #endregion
        
        // Selection Sorts
        #region Selection Sorts
        CartesianTreeSort,
        CycleSort,
        HeapSort,
        SelectionSort,
        SmoothSort,
        TournamentSort,
        WeakHeapSort,
        #endregion
        
        // Insertion Sorts
        #region Insertion Sorts
        CubeSort,
        InsertionSort,
        LibrarySort,
        PatienceSort,
        ShellSort,
        SplaySort,
        TreeSort,
        #endregion
        
        // Merge Sorts
        #region Merge Sorts
        CascadeMergeSort,
        MergeSort,
        OscillatingMergeSort,
        PolyPhaseMergeSort,
        QuadSort,
        StrandSort,
        #endregion
        
        // Distribution Sorts
        #region Distribution Sorts
        AmericanFlagSort,
        BeadSort,
        BucketSort,
        BurstSort,
        CountingSort,
        FlashSort,
        InterpolationSort,
        MultiKeyQuickSort,
        PigeonholeSort,
        ProxmapSort,
        LsdRadixSort,
        MsdRadixSort,
        #endregion
        
        // Concurrent Sorts
        #region Concurrent Sorts
        BatcherOddEvenMergeSort,
        BitonicSort,
        PairwiseSort,
        SampleSort,
        #endregion

        // Hybrid Sorts
        #region Hybrid Sorts
        BlockSort,
        IntroSort,
        KirkpatrickReischSort,
        MergeInsertionSort,
        SpreadSort,
        TimSort,
        UnShuffleSort,
        #endregion
        
        // Other Sorts
        #region Other Sorts
        FranceschiniSort,
        PancakeSort,
        SpaghettiSort,
        TopologicalSort
        #endregion
    }
}